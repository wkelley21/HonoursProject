using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client3
{
    public class UDPListener
    {
        private const int listenPort = 11000;

        //Creates a Timestamp for when messages are sent
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString(" HH:mm:ss dd/MM/yyyy");

        }

        private static void StartListener()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            String timeStamp = GetTimestamp(DateTime.Now);

            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = listener.Receive(ref groupEP);
                    Console.WriteLine($"Received broadcast from {groupEP}:");
                    Console.WriteLine($"{Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
                    string sendStr = Console.ReadLine();
                    byte[] sendbuf = Encoding.ASCII.GetBytes(sendStr + timeStamp);
                    s.SendTo(sendbuf, groupEP);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }

        
        public static void Main()
        {
            StartListener();
        }
    }
}

