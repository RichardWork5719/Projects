using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessingWithCode
{
    public class ServerTest
    {
        IPAddress IpAddress = IPAddress.Any;
        int Port = 42069;
        TcpListener server;

        List<TcpClient> clientList = new List<TcpClient>();
        bool editingClientsList = false;

        string filePath;

        public ServerTest(IPAddress? ip, int portNum = 42069)
        {
            if (ip != null)
            {
                IpAddress = ip;
            }

            Port = portNum;

            StartServer();
        }

        void StartServer()
        {
            server = new TcpListener(IpAddress, Port); // we set our IP address as server's address, and we also set the port: 9999

            server.Start();  // this will start the server

            Console.WriteLine($"Server running with address: {server.LocalEndpoint.ToString().Replace("0.0.0.0", "127.0.0.1")}"); // if the IP is 0.0.0.0 then it makes it 127.0.0.1 cuz thats how you connect to it (by copying the address into browser a connection happens)

            Console.WriteLine("\nInput the file path of the file to be imported -> example: 'D:\\folder\\folder1\\folder2\\file.csv'\n(this can be done by dragging and dropping the file into this console/window):");
            filePath = Console.ReadLine().Trim('"').Replace('\\', '/');

            ListenForClients();
            SendDataToClients();

            //Thread t1 = new Thread(ListenForClients);
            //Thread t2 = new Thread(SendDataToClients);
            //Thread t3 = new Thread(ReceiveDataFromClients);

            //t1.Start();
            //t2.Start();
            //t3.Start();
        }

        void ListenForClients()
        {
            TcpClient client = server.AcceptTcpClient();  //if a connection exists, the server will accept it
            Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");

            while (editingClientsList)
            {
                Thread.Sleep(100);
            }
            if (!editingClientsList)
            {
                editingClientsList = true;
                clientList.Add(client);
                editingClientsList = false;
            }
        }

        void SendDataToClients()
        {
            foreach (TcpClient client in clientList)
            {
                NetworkStream ns = client.GetStream(); //networkstream is used to send/receive messages

                //byte[] hello = new byte[1024];   //any message must be serialized (converted to byte array)
                byte[] hello = File.ReadAllBytes(filePath); //Encoding.Default.GetBytes("hello world");  //conversion string => byte array

                ns.Write(hello, 0, hello.Length);     //sending the message

                while (editingClientsList)
                {
                    Thread.Sleep(100);
                }
                if (!editingClientsList)
                {
                    editingClientsList = true;
                    //client.Close();
                    //clientList.Remove(client);
                    editingClientsList = false;
                }
            }
        }

        void ReceiveDataFromClients()
        {
            foreach (TcpClient client in clientList)
            {
                NetworkStream ns = client.GetStream(); //networkstream is used to send/receive messages

                byte[] msg = new byte[1024];     //the messages arrive as byte array
                ns.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
                Console.WriteLine(msg.ToString()); //now , we write the message as string
            }
        }
    }
}
