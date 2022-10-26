using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class SimpleTcpSocketServer
{
    public static void Main()
    {
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 9050);
        server.Bind(localEP);
        server.Listen(10);

        Console.WriteLine("Waiting for Client...");
        Socket client = server.Accept();
        IPAddress clientAddress = ((IPEndPoint)client.RemoteEndPoint).Address;
        Console.WriteLine("Got connection from " + clientAddress);

        byte[] data = Encoding.ASCII.GetBytes("Welcome to my test server");
        client.Send(data);

        int dataSize = 0;
        while (true)
        {
            data = new byte[1024];
            dataSize = client.Receive(data);
            if (dataSize == 0)
                break;

            Console.WriteLine(Encoding.ASCII.GetString(data, 0, dataSize));
            client.Send(data, dataSize, SocketFlags.None);
        }
        client.Close();
        server.Close();
    }
}