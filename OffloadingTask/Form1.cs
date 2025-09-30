using System.Threading;

namespace OffloadingTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //showMessage("First Button Clicked",2000);//will block the main thread

            Thread threadBtn1 = new Thread(() => showMessage("First Button Clicked", 3000));
            threadBtn1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // showMessage("Second Button Clicked", 2000);// will block the main thread
            Thread threadBtn2 = new Thread(() => showMessage("Second Button Clicked", 2000));
            threadBtn2.Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {
           
        }



        private void showMessage(string message, int delay)
        {
            Thread.Sleep(delay);
           
            lblMessage.Invoke(() => {
                lblMessage.Text = message;
            });
        }
    }
}
