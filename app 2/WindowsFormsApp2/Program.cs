using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    static class Program
    {
       
        

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
        static async Task Main()
        {
            // //Application.EnableVisualStyles();
            // //Application.SetCompatibleTextRenderingDefault(false);
            // //Application.Run(new Form1());
            // Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            // IPAddress broadcast = IPAddress.Parse("192.168.56.1");//"192.168.56.1""127.0.0.1"
            // await Task.Delay(1000);
            // IPEndPoint ep = new IPEndPoint(broadcast, 9500);

            // _ = Task.Run(() => Receive());

            // /* while loop write and read messages to and from the server.
            //send and recieve the message*/
            // try
            // {
            //     while (true)
            //     {
            //         String timeStamp = GetTimestamp(DateTime.Now);
            //         string sendStr = Console.ReadLine();
            //         byte[] sendbuf = Encoding.ASCII.GetBytes(sendStr + timeStamp);
            //         s.SendTo(sendbuf, ep);
            //         Console.WriteLine("Message sent to the broadcast address");

            //     }
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e.ToString());
            // }
            var useForm = startForm();
            _ = Task.Run(() => Form1.Receive());
            await Task.Delay(1);
            //var sendMessage = Form1.Send();
            await useForm;
            //await sendMessage;
        }
    }
}
