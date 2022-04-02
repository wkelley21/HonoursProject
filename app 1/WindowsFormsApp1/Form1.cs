using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public delegate void ChangeText_Handler(string text);
        static event ChangeText_Handler ChangeText;
        //public static string sendStr;

        private const int listenPort = 11000;
        private const int sendPort = 11111;

        
        public static string returnData;
        
        //Creates a Timestamp for when messages are sent
        //public static String GetTimestamp(DateTime value)
        //{
        //    return value.ToString(" HH:mm:ss dd/MM/yyyy");

        //}

        public static async Task Receive()
        {
            //Creates a UdpClient for reading incoming data.
            UdpClient receivingUdpClient = new UdpClient(11000);
            await Task.Delay(1);
            //Creates an IPEndPoint to record the IP Address and port number of the sender.
            // The IPEndPoint will allow you to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            //Form1 frm1 = new Form1();
            try
            {
                while (true)
                {
                    
                    // Blocks until a message returns on this socket from a remote host.
                    Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                    returnData = Encoding.ASCII.GetString(receiveBytes);
                    ChangeText(returnData);

                }
            }
            catch (Exception e)
            {
                ChangeText(e.ToString());
            }
        }

        void myChangeTextMethod(string text) 
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ChangeText_Handler(myChangeTextMethod), text);
            }
            else
            {
                if (this.textBox3.Text == "")
                {
                    this.textBox3.Text += text;
                }
                else
                {
                    this.textBox3.Text += Environment.NewLine + text;
                }
            }
        }
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress broadcast = IPAddress.Parse("192.168.56.1");
            IPEndPoint ep = new IPEndPoint(broadcast, 11111);

            try
                {
                //String timeStamp = GetTimestamp(DateTime.Now);
                string msg = textBox1.Text;
                byte[] sendbuf = Encoding.ASCII.GetBytes(msg);
                s.SendTo(sendbuf, ep);
                textBox1.Clear();
                }
            catch (Exception c)
            {
                ChangeText(c.ToString());
            }
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
    

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChangeText += myChangeTextMethod;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress broadcast = IPAddress.Parse("192.168.56.1");
            IPEndPoint ep = new IPEndPoint(broadcast, 11111);

            try
            {
                //String timeStamp = GetTimestamp(DateTime.Now);
                string msg = textBox1.Text;
                string msg2 = textBox2.Text;
                string msg3 = textBox4.Text;
                byte[] sendbuf = Encoding.ASCII.GetBytes(msg);
                byte[] sendbuf2 = Encoding.ASCII.GetBytes(msg2);
                byte[] sendbuf3 = Encoding.ASCII.GetBytes(msg3);
                s.SendTo(sendbuf, ep);
                Task.Delay(2000);
                s.SendTo(sendbuf2, ep);
                Task.Delay(2000);
                s.SendTo(sendbuf3, ep);

                textBox1.Clear();
                textBox2.Clear();
                textBox4.Clear();
            }
            catch (Exception c)
            {
                ChangeText(c.ToString());
            }

        }

        


    }
}
