using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    static class Program
    {
        
        


        private static async Task startForm() 
        {
            Application.EnableVisualStyles();
            await Task.Delay(1);
            Application.SetCompatibleTextRenderingDefault(false);
            await Task.Delay(1);
            Application.Run(new Form1());
        }

        [STAThread]
        public static async Task Main()
        {
            var useForm = startForm();
            var result1 = Form1.StartListener();
            var result2 = Form1.ReceiveFromClient();
            await useForm;
            await result1;
            await result2;
            
        }
    }
}
