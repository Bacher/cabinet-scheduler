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
using System.IO;
using System.Text.RegularExpressions;

namespace Agency40Medium
{
    public partial class MainForm : Form
    {
        private const string host = @"http://www.agency40.ru/mysystem/";
        //private const string host = @"http://127.0.0.1:1337/";

        private string cookie;

        private HttpClient client = new HttpClient();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //var client = new HttpClient();

            //string str = client.GetStringAsync(host).Result;

            var res = client.GetAsync(host).Result;

            var cookieRegex = new Regex(@"^([^;]+);");

            foreach (var t in res.Headers)
            {
                if(t.Key.Equals("Set-Cookie", StringComparison.InvariantCultureIgnoreCase))
                {
                    var value = t.Value.First();

                    var matches = cookieRegex.Match(value);

                    if (matches.Length > 0)
                    {
                        cookie = matches.Groups[1].Value.Trim();
                        MessageBox.Show(cookie);
                    }
                    break;
                }
            }

        }

        private void btnLogin2_Click(object sender, EventArgs e)
        {
            //var postData = new StringContent("login=admin&pwd=0104201201042012", Encoding.ASCII, "application/x-www-form-urlencoded");
            //"login=admin&pwd=0104201201042012"

            var formData = new List<KeyValuePair<string,string>>();
            formData.Add(new KeyValuePair<string,string>("login", "admin"));
            formData.Add(new KeyValuePair<string,string>("pwd", "0104201201042012"));

            var postData = new FormUrlEncodedContent(formData);

            var res = client.PostAsync(host + "?content=login", postData).Result;

            var req = res.Content.ReadAsByteArrayAsync().Result;

            var strr = Encoding.GetEncoding(1251).GetString(req);

            var i = 0;
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            var xmlByteArray = File.ReadAllBytes(@"d:\Projects\NewAddress\rooms_n2.xml");

            var fileData = new MultipartFormDataContent();
            var fileByteArrayContent = new ByteArrayContent(xmlByteArray);
            fileByteArrayContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/xml");

            fileData.Add(fileByteArrayContent, "xml_file", "xmltoupload.xml");
            

            var fileDataEx = new HttpRequestMessage(HttpMethod.Post, host + @"shop/?content=editor&action=loadfromfile&parent=0");

            //fileDataEx.Headers.Add("Connection", "keep-alive");
            //fileDataEx.Headers.Add("Content-Length", @"10072");
            fileDataEx.Headers.Add("Cache-Control", @"max-age=0");
            fileDataEx.Headers.Add("Accept", @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            fileDataEx.Headers.Add("Origin", @"http://www.agency40.ru");
            fileDataEx.Headers.Add("User-Agent", @"Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17");
            //fileDataEx.Headers.Add("Content-Type", @"multipart/form-data; boundary=----WebKitFormBoundary1IsqZQ5b157GjjTm");
            fileDataEx.Headers.Add("Referer", @"http://www.agency40.ru/mysystem/shop/?content=editor&action=xmlform");
            //fileDataEx.Headers.Add("Accept-Encoding", @"gzip,deflate,sdch");
            fileDataEx.Headers.Add("Accept-Language", @"en-US,en;q=0.8");
            fileDataEx.Headers.Add("Accept-Charset", @"ISO-8859-1,utf-8;q=0.7,*;q=0.3");

            fileDataEx.Content = fileData;

            var res = client.SendAsync(fileDataEx).Result;

            var str = res.Content.ReadAsStringAsync().Result;


            /*
            var res = client.PostAsync(host + @"shop/?content=editor&action=loadfromfile&parent=0", fileData).Result;

            var req = res.Content.ReadAsByteArrayAsync().Result;

            var strr = Encoding.UTF8.GetString(req);
            */
            var i = 0;
        }
    }
}
