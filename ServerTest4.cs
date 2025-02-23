using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MessingWithCode
{
    public static class WebServer
    {
        static void StartServerTest4(string[] args) // just use this function to start the webserver
        {
            try
            {
                //args = ["127.0.0.300", "66666"]; // left here for testing arguments

                string serverIP = ServerInfoParser.FormatIPAddress(args);
                int serverPort = ServerInfoParser.FormatPortNumber(args);

                ServerTest4 server = new ServerTest4(IPAddress.Parse(serverIP), serverPort); // IPAddress.Parse("127.0.0.1")
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    public static class ServerLogger
    {
        private static readonly object _lock = new object();

        static void CheckAndMakeDiretory(string LogFilePath)
        {
            if (!Directory.Exists(LogFilePath))
            {
                Directory.CreateDirectory(LogFilePath);
            }
        }

        static void CheckAndMakeFilePath(string LogTxtFilePath)
        {
            if (!File.Exists(LogTxtFilePath))
            {
                File.Create(LogTxtFilePath);
            }
        }

        static void WriteToFile(string filePath, string message)
        {
            try
            {
                using (StreamWriter s = new StreamWriter(filePath, true))
                {
                    s.Write($"{message}\n"); // File.AppendAllText(filePath, $"{message}\n"); // $"[{threadName}][{date}-{time}]: {text}"
                    
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message); // ServerLogger.Log();
            }
        }

        public static void Log(string text) // replace all -> Console.WriteLine($"{GetCurrentThreadInfo()} <- with -> ServerLogger.Log($"
        {
            string threadName = $"Thread:{Thread.CurrentThread.ManagedThreadId}";
            string date = $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";
            string time = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.{DateTime.Now.Millisecond}.{DateTime.Now.Microsecond}";

            
            string LogFilePath = Path.Combine("ServerLogs", date.Split("/")[0], date.Split("/")[1], date.Split("/")[2]);
            string GlobalLogFile = Path.Combine(LogFilePath, $"{date.Replace("/", "-")}-Log.txt");
            string LogTxtFilePath = Path.Combine(LogFilePath, $"{threadName.Replace(":", "")}"); 
            string IndividualFilePath = Path.Combine(LogTxtFilePath, $"{time.Split(":")[0]}.txt"); // i guess we doing logs by the hour ¯\_(ツ)_/¯

            CheckAndMakeDiretory(LogFilePath);
            CheckAndMakeDiretory(LogTxtFilePath);
            CheckAndMakeFilePath(IndividualFilePath);

            bool modifiedLogFile = false;
            bool modifiedGlobalLogFile = false;

            while (!modifiedLogFile)
            {
                lock (_lock)
                {
                    string message = $"[{threadName}][{time}]: {text}";
                    WriteToFile(IndividualFilePath, message);

                    modifiedLogFile = true;
                }
            }

            while (!modifiedGlobalLogFile)
            {
                lock (_lock)
                {
                    WriteToFile(GlobalLogFile, $"[{threadName}][{date}-{time}]: {text}");

                    modifiedGlobalLogFile = true;
                }
            }

            Console.WriteLine($"[{threadName}][{date}-{time}]: {text}");
        }
    }

    public static class ServerInfoParser
    {
        public static string FormatIPAddress(string[] args)
        {
            string ipAddress = "127.0.0.1"; // the range should be from 0.0.0.1 to 127.255.255.255.254

            try
            {
                if (args.Length > 0)
                {
                    ServerLogger.Log($"Arguments:");
                    foreach (string arg in args)
                    {
                        ServerLogger.Log(arg);
                    }
                    ServerLogger.Log("");

                    ipAddress = args[0].Trim();

                    if (!IPAddress.TryParse(ipAddress, out _)) // the underscore '_' is a throwaway variable
                    {
                        ipAddress = "127.0.0.1";

                        ServerLogger.Log($"Inputted IP number is invalid. (from 0.0.0.1 to 127.255.255.255.254) Using default IP number: {ipAddress}");
                    }

                    ServerLogger.Log($"IP: {ipAddress}\n");
                }
            }
            catch (Exception ex)
            {
                ServerLogger.Log(ex.Message);
            }

            return ipAddress;
        }

        public static int FormatPortNumber(string[] args)
        {
            int port = 42069;

            try
            {
                if (args.Length > 0)
                {
                    if (args.Length > 1)
                    {
                        port = int.Parse(args[1].Trim());

                        if (port < 1 || port > 65535)
                        {
                            port = 42069;

                            ServerLogger.Log($"Inputted port number is too high or too low. (1-65535) Using default port number: {port}");
                        }
                    }

                    ServerLogger.Log($"Port: {port}\n");
                }
            }
            catch (Exception ex)
            {
                ServerLogger.Log(ex.Message);
            }

            return port;
        }
    }

    class ServerTest4
    {
        IPAddress IpAddress = IPAddress.Any;
        int Port = 42069;
        TcpListener Listener;

        public ServerTest4(IPAddress? ip, int portNum = 42069)
        {
            if (ip != null)
            {
                IpAddress = ip;
            }

            Port = portNum;

            StartServer();
        }

        string GetIPAsString(IPAddress in_ip)
        {
            string ip;

            if (in_ip == IPAddress.Any)
            {
                ip = "127.0.0.1";
            }
            else if (in_ip == IPAddress.Loopback)
            {
                ip = "localhost";
            }
            else
            {
                ip = IpAddress.ToString();
            }
            ServerLogger.Log("IP: " + ip);
            ServerLogger.Log("Port: " + Port);

            return ip;
        }

        string GetCurrentThreadInfo() // yeah kinda useless now that i have the ServerLogger class
        {
            return $"[Thread:{Thread.CurrentThread.ManagedThreadId}][{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}-{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.{DateTime.Now.Millisecond}.{DateTime.Now.Microsecond}]:";
        }

        void StartServer()
        {
            string ip = GetIPAsString(IpAddress);
            string serverAddress = $"http://{ip}:{Port}";
            Listener = new TcpListener(IpAddress, Port);
            Listener.Start();
            ServerLogger.Log($"Server started.\nConnect to it through your browser at: {serverAddress}\n");

            Thread th = new Thread(new ThreadStart(StartListen));
            th.Start();
        }

        void StartListen()
        {
            ServerLogger.Log($"Listening for clients..."); //CROWBAR

            bool running = true;
            while (running)
            {
                TcpClient client = Listener.AcceptTcpClient();
                ServerLogger.Log($"Client connected: {client.Client.RemoteEndPoint}");

                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                clientThread.Start(client);
            }
        }

        void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            string requestLine = reader.ReadLine();
            ServerLogger.Log($"Received request: {requestLine}"); //CROWBAR

            ServerLogger.Log($"Extra info:");
            string line;
            while ((line = reader.ReadLine()) != null && line != "")
            {
                ServerLogger.Log($"{line}");
            }

            string requestedFile = GetRequestedFile(requestLine);
            byte[] responseBytes = GetFileData(requestedFile);

            stream.Write(responseBytes, 0, responseBytes.Length);
            ServerLogger.Log($"Response sent"); //CROWBAR
            client.Close(); // when including <!DOCTYPE html> in the responseBytes, the connection then has to be closed. else the browser will just keep loading. to not have to use client.Close() you can just remove the <!DOCTYPE html> from the html data and remove this line too
            ServerLogger.Log($"Closed connection"); //CROWBAR
        }

        string GetRequestedFile(string request)
        {
            string filePath = $"{request.Replace("GET /", "").Replace(" HTTP/1.1", "")}";
            var formattedFilePath = Path.Combine(filePath); // hopefully correctly formatted

            return formattedFilePath;
        }

        byte[] GetFileData(string path)
        {
            string defaultFileStringData = "<!DOCTYPE html><html><body style=\"background-color:black;\" text=\"white\"><h1 style=\"position:absolute;text-align:center;width: 99%;\">Nothing here!</h1>\n<h1 style=\"position:absolute;text-align:center;top: 50%;width: 99%;color: black;\">You're Gay!</h1></body></html>"; //font-size: 500%;
            byte[] fileData = null;

            string fileType = "text/html";

            byte[] responseBytes;

            if (File.Exists(path))
            {
                try
                {
                    fileData = File.ReadAllBytes(path);

                    /* Text based shit */
                    if (path.EndsWith(".css"))
                    {
                        fileType = "text/css";
                    }
                    else if (path.EndsWith(".txt"))
                    {
                        fileType = "text/plain";
                    }
                    else if (path.EndsWith(".html"))
                    {
                        fileType = "text/html";
                    }
                    else if (path.EndsWith(".xhtml"))
                    {
                        fileType = "application/xhtml+xml";
                    }
                    else if (path.EndsWith(".js"))
                    {
                        fileType = "application/javascript"; // text/javascript <- also works but outdated so less common 
                    }
                    else if (path.EndsWith(".json"))
                    {
                        fileType = "application/json";
                    }
                    else if (path.EndsWith(".xml"))
                    {
                        fileType = "application/xml"; // text/xml <- also works but less common
                    }

                    /* Image based shit */
                    else if (path.EndsWith(".png"))
                    {
                        fileType = "image/png";
                    }
                    else if (path.EndsWith(".jpg") || path.EndsWith(".jpeg"))
                    {
                        fileType = "image/jpeg";
                    }
                    else if (path.EndsWith(".webp"))
                    {
                        fileType = "image/webp";
                    }
                    else if (path.EndsWith(".avif"))
                    {
                        fileType = "image/avif";
                    }
                    else if (path.EndsWith(".gif"))
                    {
                        fileType = "image/gif";
                    }
                    else if (path.EndsWith(".svg"))
                    {
                        fileType = "image/svg+xml";
                    }
                    else if (path.EndsWith(".bmp"))
                    {
                        fileType = "image/bmp";
                    }
                    else if (path.EndsWith(".tiff"))
                    {
                        fileType = "image/tiff";
                    }

                    /* Audio based shit */
                    else if (path.EndsWith(".mpeg"))
                    {
                        fileType = "audio/mpeg";
                    }
                    else if (path.EndsWith(".wav"))
                    {
                        fileType = "audio/wav";
                    }
                    
                    /* Between Audio and Video based shit */
                    else if (path.EndsWith(".ogg"))
                    {
                        fileType = "video/ogg"; // audio/ogg or video/ogg    <- apparently .ogg files also have a video format but IDK if making it video will just guarantee that it sends everything it needs to
                    }

                    /* Video based shit */
                    else if (path.EndsWith(".mp4"))
                    {
                        fileType = "video/mp4";
                    }
                    else if (path.EndsWith(".avi"))
                    {
                        fileType = "video/x-msvideo";
                    }

                    /* Document based shit */
                    else if (path.EndsWith(".pdf"))
                    {
                        fileType = "application/pdf";
                    }
                    else if (path.EndsWith(".doc"))
                    {
                        fileType = "application/msword";
                    }
                    else if (path.EndsWith(".docx"))
                    {
                        fileType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }
                    else if (path.EndsWith(".xls"))
                    {
                        fileType = "application/vnd.ms-excel";
                    }
                    else if (path.EndsWith(".xlsx"))
                    {
                        fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else if (path.EndsWith(".ppt"))
                    {
                        fileType = "application/vnd.ms-powerpoint";
                    }
                    else if (path.EndsWith(".pptx"))
                    {
                        fileType = "application/application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    }

                    /* Binary file based shit */
                    else if (path.EndsWith(".zip"))
                    {
                        fileType = "application/zip";
                    }
                    else if (path.EndsWith(".tar"))
                    {
                        fileType = "application/x-tar";
                    }

                    /* If all else fails then just use arbitrary binary file data */
                    else
                    {
                        fileType = "application/octet-stream";
                    }
                }
                catch (Exception ex)
                {
                    ServerLogger.Log($"Requested path most likely doesnt include a specific file name\nException message:\n{ex}");
                }
            }
            else
            {
                ServerLogger.Log($"File does not exist: {path} | Picking default file data"); //CROWBAR
            }

            if (fileData == null)
            {
                responseBytes = Encoding.UTF8.GetBytes($"HTTP/1.1 200 OK\r\nContent-Type: text/html\r\nContent-Length: {defaultFileStringData.Length}\r\n\r\n{defaultFileStringData}");
            }
            else
            {
                byte[] fileHeader = Encoding.UTF8.GetBytes($"HTTP/1.1 200 OK\r\nContent-Type: {fileType}\r\nContent-Length: {fileData.Length}\r\n\r\n");

                var memoryStream = new MemoryStream();
                memoryStream.Write(fileHeader, 0, fileHeader.Length);
                memoryStream.Write(fileData, 0, fileData.Length);
                responseBytes = memoryStream.ToArray();
            }

            return responseBytes;
        }
    }
}
