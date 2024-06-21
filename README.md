Place the file containing the ip addresses separated by new lines to the PingAssignment/bin/Debug folder. The program will ping these.
Start the program with your file name as an argument for it.
If the ping is success the program prints out "Success". After this it will sends a HTTP get request to the port 8080 or if some problem occurs then tries the port 80.
If this operation was successfull the program prints out "HTTP 8080/80 is open / not open".

I provided a simple "server" with the name LocalServer. This listens on 127.0.0.1:8080. (It will sends back a "HELLO THERE" message.)

For pinging I used the Ping class, and HttpClient for sending a HTTP request.
