using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WindowsFormsApp3
{
    
    public partial class Form1 : Form
    {
        public delegate void ChangeText_Handler(string text);
        static event ChangeText_Handler ChangeText;
        public static Form1 instance;
        private static bool myRandomBool;
        private const int listenPort = 9500;
        private const int listenerPort = 11111;
        


        public static async Task StartListener()
        {
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            Socket s1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress broadcast = IPAddress.Parse("192.168.1.255");
            IPEndPoint ep = new IPEndPoint(broadcast, 11000);

            try
            {
                int messageIndex = 0;
                Array myArray = Array.CreateInstance(typeof(string), 3);

                while (true)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    string sendStr = ($"{Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
                    await Task.Delay(1);
                    byte[] sendbuf = Encoding.ASCII.GetBytes(sendStr);
                    if (instance.checkBox1.Checked)
                    {

                        var delay = instance.getDelay();
                        await Task.Delay(delay);
                        var delayString = "There has been a delay of " + delay.ToString() + "ms to the packet ";
                        //byte[] sendDelay = Encoding.ASCII.GetBytes(delayString);
                        //s.SendTo(sendDelay, ep);
                        ChangeText(delayString);
                        

                    }
                    int messageSendCount = 1;
                    if (instance.checkBox2.Checked)
                    {
                        messageSendCount = instance.getMessageSendCount();
                    }
                    for (int i = 0; i < messageSendCount; i++)
                    {
                        if (instance.checkBox3.Checked)
                        {
                            myRandomBool = new Random().Next(100) <= instance.getLossChance() ? true : false;
                            if (myRandomBool)
                            {

                                var lostString2 = "The packet received from the server has been lost ";
                                ChangeText(lostString2);
                                //byte[] sendLost = Encoding.ASCII.GetBytes(lostString);
                                //s.SendTo(sendLost, ep);

                            }
                            else
                            {
                                s1.SendTo(sendbuf, ep);
                            }

                        }
                        else if (instance.checkBox4.Checked)
                        {

                            myArray.SetValue(sendStr, messageIndex);
                            if (messageIndex == 2)
                            {
                                myRandomBool = new Random().Next(100) <= instance.getReOredrLossChance() ? true : false;
                                if (myRandomBool)
                                {
                                    // Reverses the sort of the values of the Array.
                                    Array.Reverse(myArray, 0, 3);

                                }

                                foreach (string message in myArray)
                                {
                                    s1.SendTo(Encoding.ASCII.GetBytes(message), ep);
                                    //var reOrderString = "The packets have been reordered";
                                    //ChangeText(reOrderString);
                                }
                            }
                        }
                        else
                        {
                            s1.SendTo(sendbuf, ep);
                        }

                    }
                    messageIndex++;
                    if (messageIndex == 3)
                    {
                        messageIndex = 0;
                    }

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

        public static async Task ReceiveFromClient()
        {
            UdpClient udpClient = new UdpClient(listenerPort);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, listenerPort);
            Socket s2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress sendBack = IPAddress.Parse("192.168.56.1");
            IPEndPoint serverEP = new IPEndPoint(sendBack, 9551);

            try
            {
                int messageIndex = 0;
                Array myArray = Array.CreateInstance(typeof(string), 3);

                while (true)
                {
                    byte[] bytes = udpClient.Receive(ref remoteEP);
                    await Task.Delay(1);
                    string sendStr = ($"{Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
                    await Task.Delay(1);
                    byte[] sendbuf = Encoding.ASCII.GetBytes(sendStr);
                    
                    if (instance.checkBox1.Checked)
                    {
                        var delay = instance.getDelay();
                        await Task.Delay(delay);
                        var delayString = "There has been a delay of " + delay.ToString() + "ms to the packet";
                        //byte[] sendDelay = Encoding.ASCII.GetBytes(delayString);
                        //s2.SendTo(sendDelay, serverEP);
                        ChangeText(delayString);
                        
                    }
                    int messageSendCount = 1;
                    if (instance.checkBox2.Checked) 
                    {
                        messageSendCount = instance.getMessageSendCount();
                    }
                    for (int i= 0; i<messageSendCount; i++) 
                    {
                        if (instance.checkBox3.Checked)
                        {
                            myRandomBool = new Random().Next(100) <= instance.getLossChance() ? true : false;
                            if (myRandomBool)
                            {
                                //byte[] sendLost = Encoding.ASCII.GetBytes(lostString);
                                //s2.SendTo(sendLost, serverEP);
                                var lostString = "The packet received from the client has been lost ";
                                ChangeText(lostString);
                               
                            }
                            else
                            {
                                s2.SendTo(sendbuf, serverEP);
                            }

                        }
                        else if (instance.checkBox4.Checked) 
                        {
                            
                            myArray.SetValue(sendStr, messageIndex);
                            if (messageIndex == 2)
                            {
                                myRandomBool = new Random().Next(100) <= instance.getReOredrLossChance() ? true : false;
                                if (myRandomBool)
                                {
                                    // Reverses the sort of the values of the Array.
                                    Array.Reverse(myArray, 0, 3);

                                }

                                foreach(string message in myArray) 
                                {
                                    s2.SendTo(Encoding.ASCII.GetBytes(message), serverEP);
                                    //var reOrderString = "The packets have been reordered";
                                    //ChangeText(reOrderString);
                                }
                            }
                        }
                        else
                        {
                            s2.SendTo(sendbuf, serverEP);
                        }
                        
                    }
                    messageIndex++;
                    if (messageIndex == 3) 
                    {
                        messageIndex = 0;
                    }
                    
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                udpClient.Close();

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
                if (this.richTextBox1.Text == "")
                {
                    this.richTextBox1.Text += text;
                    this.richTextBox1.ForeColor = Color.Red;
                }
                else
                {
                    this.richTextBox1.Text += Environment.NewLine + text;
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            instance = this;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public int getDelay() 
        {
           
            var delay = Int32.Parse(instance.textBox2.Text);
            return delay;
        }

        public int getMessageSendCount()
        {

            var MesSendCon = Int32.Parse(instance.textBox1.Text);
            return MesSendCon;
        }

        public int getLossChance()
        {

            var LossChance = Int32.Parse(instance.textBox3.Text);
            return LossChance;
        }

        public int getReOredrLossChance()
        {

            var reOrderLossChance = Int32.Parse(instance.textBox4.Text);
            return reOrderLossChance;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "^[1-9][0-9]?$|^100$"))
            //{
            //    string warningMsg ="Please enter only numbers.";
            //    ChangeText(warningMsg);
            //    textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1);
            //}
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChangeText += myChangeTextMethod;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
