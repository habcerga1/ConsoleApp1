using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public  class Request
    {
        public static async Task GetHttpClient(int index)
        {
            var httpclient = new HttpClient();
            var st1 = Stopwatch.StartNew();
            var response = await httpclient.GetAsync("https://mail.ru",
                HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            st1.Stop();
            string text = null;
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var bytes = new byte[1000];
                var bytesread = stream.Read(bytes, 0, 1000);
                stream.Close();
                
                text = Encoding.UTF8.GetString(bytes);
                
                Console.Write($"{index} {st1.Elapsed} || ");
            }
        }
        
        public static async Task GetHttpWebClient(int index)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            var st1 = Stopwatch.StartNew();
            Stream responseStream = webClient.OpenRead("http://www.google.com");
            if (responseStream != null)
            {
                st1.Stop();
                var responseReader = new StreamReader(responseStream);
                string response = responseReader.ReadToEnd();
                Console.WriteLine($"{index} {st1.Elapsed} || ");
                Console.WriteLine(response);
            }
        }
        
        public  async Task GetHttpWebRequestAsync(int index)
        {
            string url = "http://www.rambler.ru";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Proxy = null;
            var st1 = Stopwatch.StartNew();
            var response = await request.GetResponseAsync().ConfigureAwait(false);
            st1.Stop();
            Stream resStream = response.GetResponseStream();
            if (resStream != null)
            {
                st1.Stop();
                var responseReader = new StreamReader(resStream);
                string re = responseReader.ReadToEnd();
                Console.WriteLine($"{index} {st1.Elapsed} || ");
                //Console.WriteLine(re);
            }
        }

        public static async Task GetHttpSocketAsync(int index)
        {
            byte[] bytes = new byte[1024];  
  
        try  
        {  
            // Connect to a Remote server  
            // Get Host IP Address that is used to establish a connection  
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            // If a host has multiple addresses, you will get a list of addresses  
            IPHostEntry host = Dns.GetHostEntry(new Uri("http://www.google.com").Host);  
            IPAddress ipAddress = host.AddressList[0];  
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 80);  
  
            // Create a TCP/IP  socket.    
            Socket sender = new Socket(ipAddress.AddressFamily,  
                SocketType.Stream, ProtocolType.Tcp);  
  
            // Connect the socket to the remote endpoint. Catch any errors.    
            try  
            {  
                var st1 = Stopwatch.StartNew();
                // Connect to Remote EndPoint  
                sender.Connect(remoteEP);  
  
                Console.WriteLine("Socket connected to {0}",  
                    sender.RemoteEndPoint.ToString());  
                Console.WriteLine($"{index} {st1.Elapsed} || ");
                // Encode the data string into a byte array.    
                byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");  
  
                // Send the data through the socket.    
                int bytesSent = sender.Send(msg);  
  
                // Receive the response from the remote device.    
                int bytesRec = sender.Receive(bytes);  
                Console.WriteLine("Echoed test = {0}",  
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));  
  
                // Release the socket.    
                sender.Shutdown(SocketShutdown.Both);  
                sender.Close();  
  
            }  
            catch (ArgumentNullException ane)  
            {  
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());  
            }  
            catch (SocketException se)  
            {  
                Console.WriteLine("SocketException : {0}", se.ToString());  
            }  
            catch (Exception e)  
            {  
                Console.WriteLine("Unexpected exception : {0}", e.ToString());  
            }  
  
        }  
        catch (Exception e)  
        {  
            Console.WriteLine(e.ToString());  
        }  
        }
    }
}