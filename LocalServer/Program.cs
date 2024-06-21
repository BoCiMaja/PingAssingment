using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LocalServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            //TcpListener listener = new TcpListener(localAddr, 8080);

            //listener.Start();

            //TcpClient client = listener.AcceptTcpClient();

            //Console.ReadLine();
            //client.Close();

            HttpListener httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://127.0.0.1:8080/");
            httpListener.Start();

            HttpListenerContext context = httpListener.GetContext();
            byte[] output = Encoding.ASCII.GetBytes("HELLO THERE");
            context.Response.ContentEncoding = Encoding.ASCII;
            context.Response.ContentLength64 = output.Length;
            context.Response.OutputStream.Write(output, 0, output.Length);

            Console.ReadLine();
            httpListener.Close();
        }
    }
}
