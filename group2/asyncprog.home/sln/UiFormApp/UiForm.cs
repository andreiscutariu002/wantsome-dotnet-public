﻿namespace UiFormApp
{
    using System.Diagnostics;
    using System.Net;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class UiForm : Form
    {
        public UiForm()
        {
            this.InitializeComponent();
        }

        private void DownloadBtnLeft_Click(object sender, System.EventArgs e)
        {
            var url = this.urlTextBoxLeft.Text;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var source = this.DownloadString(url);

            this.contentTxbLeft.Text = source;
            this.logLabelLeft.Text = $@"Downloaded in {stopwatch.ElapsedMilliseconds} ms";
        }

        private async void DownloadBtnRight_Click(object sender, System.EventArgs e)
        {
            var url = this.urlTextBoxRight.Text;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Task<string> task = new WebClient().DownloadStringTaskAsync(url);
            var source = await task;
            //var source = await this.DownloadStringAsync(url);

            this.contentTxbRight.Text = source;
            this.logLabelRight.Text = $@"Downloaded in {stopwatch.ElapsedMilliseconds} ms";
        }

        private string DownloadString(string url)
        {
            return new WebClient().DownloadString(url);
        }

        private Task<string> DownloadStringAsync(string url)
        {
            return Task.Run(() => DownloadString(url));
        }
    }
}