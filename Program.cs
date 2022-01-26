using System;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace webserverpage
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Prefixes.Add("http://localhost:8080/scripts.js/");
            listener.Prefixes.Add("http://localhost:8080/styles.css/");
            listener.Start();
            System.Console.WriteLine("Listening on port 8080...");


            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();

                if (context == null) continue;

                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                System.Console.WriteLine("Request received!");
                System.Console.WriteLine("Requested URL: " + request.RawUrl);
                System.Console.WriteLine("Requested method: " + request.HttpMethod);
                System.Console.WriteLine("Requested headers:");
                foreach (string header in request.Headers)
                {
                    System.Console.WriteLine("\t" + header + ": " + request.Headers[header]);
                }
                byte[] buffer;

                if (request.HttpMethod == "GET")
                {
                    switch (request.RawUrl)
                    {
                        case "/":
                            response.StatusCode = 200;
                            response.ContentType = "text/html";
                            string indexResponse = File.ReadAllText(@"index.html");
                            buffer = System.Text.Encoding.UTF8.GetBytes(indexResponse);
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                            response.OutputStream.Close();
                            break;

                        case "/scripts.js/":
                            response.StatusCode = 200;
                            response.ContentType = "text/javascript";
                            string scriptsResponse = File.ReadAllText(@"scripts.js");
                            buffer = System.Text.Encoding.UTF8.GetBytes(scriptsResponse);
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                            response.OutputStream.Close();
                            break;

                        case "/styles.css/":
                            response.StatusCode = 200;
                            response.ContentType = "text/css";
                            string stylesResponse = File.ReadAllText(@"styles.css");
                            buffer = System.Text.Encoding.UTF8.GetBytes(stylesResponse);
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                            response.OutputStream.Close();
                            break;
                    }
                }
                else if (request.HttpMethod == "POST")
                {
                    string data = new StreamReader(request.InputStream).ReadToEnd();

                    System.Console.WriteLine("POST data: " + data);
                }
            }
        }
    }
}
