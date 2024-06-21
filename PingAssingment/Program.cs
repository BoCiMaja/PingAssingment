using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PingAssingment
{
    internal class Program
    {
        public static async Task doTask(string ipAddress)
        {
            Ping pinger = new Ping();
            PingReply reply;
            do
            {
                reply = pinger.Send(ipAddress);
                Console.WriteLine("Pinging {0}, status: {1}", ipAddress, reply.Status);
                Thread.Sleep(1000);
            }
            while (reply.Status != IPStatus.Success);
            pinger.Dispose();

            try
            {
                HttpClient client = new HttpClient();
                UriBuilder builder = new UriBuilder(ipAddress + ":8080");
                client.BaseAddress = builder.Uri;
                HttpResponseMessage response = await client.GetAsync(builder.Uri);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                { 
                    if (ipAddress == "127.0.0.1")
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"{jsonResponse}\n");
                    }

                    Console.WriteLine("HTTP port {0}:8080 is open!", ipAddress);
                }
                else
                    Console.WriteLine("HTTP port {0}:8080 is not open!", ipAddress);
            } catch (Exception)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    UriBuilder builder = new UriBuilder(ipAddress + ":80");
                    client.BaseAddress = builder.Uri;
                    HttpResponseMessage _ = await client.GetAsync(builder.Uri);
                    Console.WriteLine("HTTP port {0}:8080 is open!", ipAddress);
                } catch(Exception)
                {
                    Console.WriteLine("HTTP port {0}:8080 is not open!", ipAddress);
                }
            }
        }


        static void Main(string[] args)
        {
            Thread.Sleep(1000);
            List<string> ipAddresses = new List<string>();

            #region Read IP Addresses from File
            string filename = args[0];
            StreamReader reader = new StreamReader(filename);

            while(!reader.EndOfStream)
            {
                ipAddresses.Add(reader.ReadLine());
            }

            reader.Close();
            #endregion

            //Task.WhenAll(ipAddresses.Select(address => doTask(address)));
            Parallel.ForEach(ipAddresses, async address => await doTask(address));

            Console.ReadLine();
        }
    }
}
