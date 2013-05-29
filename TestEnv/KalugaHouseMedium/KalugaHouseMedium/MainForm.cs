using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace KalugaHouseMedium
{
    public partial class MainForm : Form
    {

        private string host = @"http://www.kalugahouse.ru/cabinet/";
        private HttpClient client = new HttpClient();
        private Regex rg = new Regex(@"<td[^>]*><a href=""/item/(\d+)/"">(\d+)</a></td>");
        private string _id;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var res = client.GetAsync(host).Result;

            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("email", "now.adres@yandex.ru"));
            formData.Add(new KeyValuePair<string, string>("password", "9065062255"));

            var postData = new FormUrlEncodedContent(formData);

            res = client.PostAsync(host + "?content=login", postData).Result;

            if (res.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Login Failed");
            }
        }

        private void btnParseId_Click(object sender, EventArgs e)
        {
            var res = client.GetAsync(host + @"?keywords=30657").Result;

            if (res.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Bad Code Request");
                return;
            }

            var responseBytes = res.Content.ReadAsByteArrayAsync().Result;

            var responseString = Encoding.GetEncoding(1251).GetString(responseBytes);

            var posOfStartTable = responseString.IndexOf(@"<table class=""user-items"">");  // Check -1
            responseString = responseString.Substring(posOfStartTable);

            var posOfEndTable = responseString.IndexOf(@"</table>");    // Check -1
            responseString = responseString.Substring(0, posOfEndTable);

            var matches = rg.Match(responseString);

            if(matches.Groups[1].Value != matches.Groups[2].Value)
            {
                MessageBox.Show("Bad id");
            }

            _id = matches.Groups[1].Value;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // /?content=itemdel&item=466827 

            var res = client.GetAsync(host + @"?content=itemdel&item=" + _id).Result;

            if (res.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Removing maybe failed");
            }
        }
    }
}
