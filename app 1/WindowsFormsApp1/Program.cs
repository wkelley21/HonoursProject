using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    static class Program
    {
        ///// <summary>
        ///// The main entry point for the application.
        ///// </summary>

        //private const int listenPort = 11000;
        //private const int sendPort = 11111;

        //static Form1 f;


        ////Creates a Timestamp for when messages are sent
        //public static String GetTimestamp(DateTime value)
        //{
        //    return value.ToString(" HH:mm:ss dd/MM/yyyy");

        //}

        //public static async Task Receive()
        //{
        //    //Creates a UdpClient for reading incoming data.
        //    UdpClient receivingUdpClient = new UdpClient(11000);
        //    await Task.Delay(1);
        //    //Creates an IPEndPoint to record the IP Address and port number of the sender.
        //    // The IPEndPoint will allow you to read datagrams sent from any source.
        //    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        //    //await Task.Delay(1000);
        //    try
        //    {
        //        while (true)
        //        {

        //            // Blocks until a message returns on this socket from a remote host.
        //            Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

        //            string returnData = Encoding.ASCII.GetString(receiveBytes);

        //            Console.WriteLine("This is the message you received " +
        //                                      returnData.ToString());
        //            f.getString(returnData);
        //            Console.WriteLine("This message was sent from " +
        //                                        RemoteIpEndPoint.Address.ToString() +
        //                                        " on their port number " +
        //                                        RemoteIpEndPoint.Port.ToString());
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}

        //public static async Task Send()
        //{

        //    Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //    IPAddress broadcast = IPAddress.Parse("192.168.56.1");
        //    await Task.Delay(1000);
        //    IPEndPoint ep = new IPEndPoint(broadcast, 11111);



        //    /* while loop write and read messages to and from the server.
        //   send and recieve the message*/
        //    try
        //    {
        //        while (true)
        //        {
        //            String timeStamp = GetTimestamp(DateTime.Now);
        //            _ = Form1.sendStr;
        //            //string sendStr = Console.ReadLine();
        //            byte[] sendbuf = Encoding.ASCII.GetBytes(Form1.sendStr + timeStamp);
        //            s.SendTo(sendbuf, ep);
        //            Console.WriteLine("Message sent to the broadcast address");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}
       
        private static async Task startForm() 
        {
            Application.EnableVisualStyles();
            await Task.Delay(1);
            Application.SetCompatibleTextRenderingDefault(false);
            await Task.Delay(1);
            //f = new Form1();
            Application.Run(new Form1());

        }

        [STAThread]
        public static async Task Main()
        {
            
            var useForm = startForm();
            _ = Task.Run(() => Form1.Receive());
            await Task.Delay(1);
            //var sendMessage = Form1.Send();
            await useForm;
            //await sendMessage;


        }
    }
}
