using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessingWithCode
{
    // MyWebServer Written by Imtiaz Alam
    namespace Imtiaz
    {
        using System;
        using System.IO;
        using System.Net;
        using System.Net.Sockets;
        using System.Text;
        using System.Threading;
        class MyWebServer
        {
            private TcpListener myListener;
            private int port = 42069; // Select any free port you wish
                                     // The constructor which makes the TcpListener start listening on the given port.
                                     // It also calls a Thread on the method StartListen().
            public MyWebServer(int new_port = 42069)
            {
                CheckForDirectoryPath();

                if(port != new_port)
                {
                    port = new_port;
                }

                try
                {
                    // Start listening on the given port
                    myListener = new TcpListener(port);
                    myListener.Start();
                    Console.WriteLine("Web Server Running... Press ^C to Stop...");
                    // Start the thread which calls the method 'StartListen'
                    Thread th = new Thread(new ThreadStart(StartListen));
                    th.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine("An Exception Occurred while Listening: " + e.ToString());
                }
            }

            static void CheckForDirectoryPath()
            {
                string ServerDir = Path.Combine("ServerShit", "ServerShit");
                string DataDir = Path.Combine(ServerDir, "Data");

                if (!Directory.Exists(DataDir))
                {
                    Directory.CreateDirectory(DataDir);
                }

                if (!File.Exists(Path.Combine(DataDir, "Mimes.Dat")))
                {
                    File.Create(Path.Combine(DataDir, "Mimes.Dat")).Close();
                    File.WriteAllText(Path.Combine(DataDir, "Mimes.Dat"), ".html; text/html\r\n.htm; text/html\r\n.bmp; image/bmp\r\n.png; image/png\r\n.jpg; image/jpg");
                }

                if (!File.Exists(Path.Combine(DataDir, "VDirs.Dat")))
                {
                    File.Create(Path.Combine(DataDir, "VDirs.Dat")).Close();
                    File.WriteAllText(Path.Combine(DataDir, "VDirs.Dat"), "/; " + Path.Combine(Directory.GetCurrentDirectory(), ServerDir) + "/\n" + "test/; " + Path.Combine(Directory.GetCurrentDirectory(), ServerDir, "Stuff"));
                    //  /; R:\Programs\C#\C# ButNotForUnity\ActuallyLearningC#\OtherTutorials\MessingWithCode(New)\MessingWithCode(New)\bin\Debug\net8.0\ServerShit\ServerShit\
                    //  test /; R:\Programs\C#\C# ButNotForUnity\ActuallyLearningC#\OtherTutorials\MessingWithCode(New)\MessingWithCode(New)\bin\Debug\net8.0\ServerShit\ServerShit\Stuff
                }

                if (!File.Exists(Path.Combine(DataDir, "Default.Dat")))
                {
                    File.Create(Path.Combine(DataDir, "Default.Dat")).Close();
                    File.WriteAllText(Path.Combine(DataDir, "Default.Dat"), "default.html\r\ndefault.htm\r\nIndex.html\r\nIndex.htm;");
                }
            }

            public string GetTheDefaultFileName(string sLocalDirectory)
            {
                StreamReader sr;
                string sLine = "";
                try
                {
                    // Open the default.dat to find out the list
                    // of default file
                    sr = new StreamReader("data\\Default.Dat");
                    while ((sLine = sr.ReadLine()) != null)
                    {
                        // Look for the default file in the web server root folder
                        if (File.Exists(Path.Combine(sLocalDirectory, sLine)))
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("An Exception Occurred: " + e.ToString());
                }

                if (File.Exists(Path.Combine(sLocalDirectory, sLine)))
                    return sLine;
                else
                    return "";
            }

            public string GetLocalPath(string sMyWebServerRoot, string sDirName)
            {
                StreamReader sr;
                string sLine = "";
                string sVirtualDir = "";
                string sRealDir = "";
                int iStartPos = 0;
                // Remove extra spaces
                sDirName = sDirName.Trim();
                // Convert to lowercase
                sMyWebServerRoot = sMyWebServerRoot.ToLower();
                sDirName = sDirName.ToLower();
                try
                {
                    // Open the Vdirs.dat to find out the list virtual directories
                    sr = new StreamReader("data\\VDirs.Dat");
                    while ((sLine = sr.ReadLine()) != null)
                    {
                        // Remove extra spaces
                        sLine = sLine.Trim();
                        if (sLine.Length > 0)
                        {
                            // Find the separator
                            iStartPos = sLine.IndexOf(";");
                            // Convert to lowercase
                            sLine = sLine.ToLower();
                            sVirtualDir = sLine.Substring(0, iStartPos);
                            sRealDir = sLine.Substring(iStartPos + 1);
                            if (sVirtualDir == sDirName)
                            {
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("An Exception Occurred: " + e.ToString());
                }

                if (sVirtualDir == sDirName)
                    return sRealDir;
                else
                    return "";
            }

            public string GetMimeType(string sRequestedFile)
            {
                StreamReader sr;
                string sLine = "";
                string sMimeType = "";
                string sFileExt = "";
                string sMimeExt = "";
                // Convert to lowercase
                sRequestedFile = sRequestedFile.ToLower();
                int iStartPos = sRequestedFile.IndexOf(".");
                sFileExt = sRequestedFile.Substring(iStartPos);
                try
                {
                    // Open the Mime.Dat to find out the list of MIME types
                    sr = new StreamReader("data\\Mime.Dat");
                    while ((sLine = sr.ReadLine()) != null)
                    {
                        sLine = sLine.Trim();
                        if (sLine.Length > 0)
                        {
                            // Find the separator
                            iStartPos = sLine.IndexOf(";");
                            // Convert to lowercase
                            sLine = sLine.ToLower();
                            sMimeExt = sLine.Substring(0, iStartPos);
                            sMimeType = sLine.Substring(iStartPos + 1);
                            if (sMimeExt == sFileExt)
                            {
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("An Exception Occurred: " + e.ToString());
                }

                if (sMimeExt == sFileExt)
                    return sMimeType;
                else
                    return "";
            }

            public void SendHeader(string sHttpVersion, string sMIMEHeader, int iTotBytes, string sStatusCode, ref Socket mySocket)
            {
                string sBuffer = "";
                // if Mime type is not provided set default to text/html
                if (sMIMEHeader.Length == 0)
                {
                    sMIMEHeader = "text/html"; // Default Mime Type is text/html
                }
                sBuffer += sHttpVersion + sStatusCode + "\r\n";
                sBuffer += "Server: cx1193719-b\r\n";
                sBuffer += "Content-Type: " + sMIMEHeader + "\r\n";
                sBuffer += "Accept-Ranges: bytes\r\n";
                sBuffer += "Content-Length: " + iTotBytes + "\r\n\r\n";
                byte[] bSendData = Encoding.ASCII.GetBytes(sBuffer);
                SendToBrowser(bSendData, ref mySocket);
                Console.WriteLine("Total Bytes: " + iTotBytes.ToString());
            }

            public void SendToBrowser(string sData, ref Socket mySocket)
            {
                SendToBrowser(Encoding.ASCII.GetBytes(sData), ref mySocket);
            }
            public void SendToBrowser(byte[] bSendData, ref Socket mySocket)
            {
                int numBytes = 0;
                try
                {
                    if (mySocket.Connected)
                    {
                        numBytes = mySocket.Send(bSendData, bSendData.Length, 0);
                        if (numBytes == -1)
                            Console.WriteLine("Socket Error: Cannot Send Packet");
                        else
                            Console.WriteLine("No. of bytes sent: {0}", numBytes);
                    }
                    else
                    {
                        Console.WriteLine("Connection Dropped....");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error Occurred: {0}", e);
                }
            }

            public void StartListen()
            {
                string ServerDir = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("ServerShit", "ServerShit"));  // added these two myself
                string DataDir = Path.Combine(ServerDir, "Data");

                int iStartPos = 0;
                String sRequest;
                String sDirName;
                String sRequestedFile;
                String sErrorMessage;
                String sLocalDir;
                String sMyWebServerRoot = ServerDir + "\\";
                String sPhysicalFilePath = "";
                String sFormattedMessage = "";
                String sResponse = "";
                while (true)
                {
                    // Accept a new connection
                    Socket mySocket = myListener.AcceptSocket();
                    Console.WriteLine("Socket Type " + mySocket.SocketType);
                    if (mySocket.Connected)
                    {
                        Console.WriteLine("\nClient Connected!!\n==================\nClient IP {0}\n", mySocket.RemoteEndPoint);
                        // make a byte array and receive data from the client
                        Byte[] bReceive = new Byte[1024];
                        int i = mySocket.Receive(bReceive, bReceive.Length, 0);
                        // Convert Byte to String
                        string sBuffer = Encoding.ASCII.GetString(bReceive);
                        // At present we will only deal with GET type
                        if (sBuffer.Substring(0, 3) != "GET")
                        {
                            Console.WriteLine("Only Get Method is supported..");
                            mySocket.Close();
                            return;
                        }
                        // Look for HTTP request
                        iStartPos = sBuffer.IndexOf("HTTP", 1);
                        // Get the HTTP text and version e.g. it will return "HTTP/1.1"
                        string sHttpVersion = sBuffer.Substring(iStartPos, 8);
                        // Extract the Requested Type and Requested file/directory
                        sRequest = sBuffer.Substring(0, iStartPos - 1);
                        // Replace backslash with Forward Slash, if Any
                        sRequest = sRequest.Replace("\\", "/");
                        // If file name is not supplied add forward slash to indicate
                        // that it is a directory and then we will look for the
                        // default file name..
                        if ((sRequest.IndexOf(".") < 1) && (!sRequest.EndsWith("/")))
                        {
                            sRequest = sRequest + "/";
                        }
                        // Extract the requested file name
                        iStartPos = sRequest.LastIndexOf("/") + 1;
                        sRequestedFile = sRequest.Substring(iStartPos);
                        // Extract The directory Name
                        sDirName = sRequest.Substring(sRequest.IndexOf("/"), sRequest.LastIndexOf("/") - 3);

                        /////////////////////////////////////////////////////////////////////
                        // Identify the Physical Directory
                        /////////////////////////////////////////////////////////////////////
                        if (sDirName == "/")
                            sLocalDir = sMyWebServerRoot;
                        else
                        {
                            // Get the Virtual Directory
                            sLocalDir = GetLocalPath(sMyWebServerRoot, sDirName);
                        }
                        Console.WriteLine("Directory Requested : " + sLocalDir);
                        // If the physical directory does not exist then
                        // display the error message
                        if (sLocalDir.Length == 0)
                        {
                            sErrorMessage = "<H2>Error!! Requested Directory does not exist</H2><Br>";
                            //sErrorMessage = sErrorMessage + "Please check data\\Vdirs.Dat";
                            // Format The Message
                            SendHeader(sHttpVersion, "", sErrorMessage.Length, " 404 Not Found", ref mySocket);
                            // Send to the browser
                            SendToBrowser(sErrorMessage, ref mySocket);
                            mySocket.Close();
                            continue;
                        }

                        /////////////////////////////////////////////////////////////////////
                        // Identify the File Name
                        /////////////////////////////////////////////////////////////////////
                        // If The file name is not supplied then look in the default file list
                        if (sRequestedFile.Length == 0)
                        {
                            // Get the default filename
                            sRequestedFile = GetTheDefaultFileName(sLocalDir);
                            if (sRequestedFile == "")
                            {
                                sErrorMessage = "<H2>Error!! No Default File Name Specified</H2>";
                                SendHeader(sHttpVersion, "", sErrorMessage.Length, " 404 Not Found", ref mySocket);
                                SendToBrowser(sErrorMessage, ref mySocket);
                                mySocket.Close();
                                return;
                            }
                        }

                        /////////////////////////////////////////////////////////////////////
                        // Get The Mime Type
                        /////////////////////////////////////////////////////////////////////
                        String sMimeType = GetMimeType(sRequestedFile);
                        // Build the physical path
                        sPhysicalFilePath = sLocalDir + sRequestedFile;
                        Console.WriteLine("File Requested : " + sPhysicalFilePath);

                        if (File.Exists(sPhysicalFilePath) == false)
                        {
                            sErrorMessage = "<H2>404 Error! File Does Not Exist...</H2>";
                            SendHeader(sHttpVersion, "", sErrorMessage.Length, " 404 Not Found", ref mySocket);
                            SendToBrowser(sErrorMessage, ref mySocket);
                            Console.WriteLine(sFormattedMessage);
                        }
                        else
                        {
                            int iTotBytes = 0;
                            sResponse = "";
                            FileStream fs = new FileStream(sPhysicalFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                            // Create a reader that can read bytes from the FileStream.
                            BinaryReader reader = new BinaryReader(fs);
                            byte[] bytes = new byte[fs.Length];
                            int read;
                            while ((read = reader.Read(bytes, 0, bytes.Length)) != 0)
                            {
                                // Read from the file and write the data to the network.
                                sResponse = sResponse + Encoding.ASCII.GetString(bytes, 0, read);
                                iTotBytes = iTotBytes + read;
                            }
                            reader.Close();
                            fs.Close();
                            SendHeader(sHttpVersion, sMimeType, iTotBytes, " 200 OK", ref mySocket);
                            SendToBrowser(bytes, ref mySocket);
                            //mySocket.Send(bytes, bytes.Length, 0);
                        }
                        mySocket.Close();
                    }
                }
            }
        }
    }
}
