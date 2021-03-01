using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwaitPractice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static async Task<string> UpdateText()
        {
            await Task.Delay(5000);
            return "Updated";
        }
        
        private async void btnClickMe_Click(object sender, EventArgs e)
        {
            var updateTask = await UpdateText();
            lblText.Text = updateTask;

            await Task.Delay(5000);

            using (var wc = new WebClient())
            {
                string data =
                    await wc.DownloadStringTaskAsync("http://google.com/robots.txt");
                lblText.Text = data.Split("\n")[0].Trim();
            }
        }
    }
}