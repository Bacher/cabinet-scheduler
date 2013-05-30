using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Xml;
using System.IO;

namespace Medium
{
    public class Agency40Medium
    {
        private HttpClient _webClient = new HttpClient();
        private string _hostUrl;
        public bool logged = false;

        public Agency40Medium(string url)
        {
            _hostUrl = url;
        }

        public void Login(string username, string password)
        {
            HttpContent formData = prepareLoginFormData(username, password);

            var res = _webClient.PostAsync(_hostUrl + "?content=login", formData).Result;
           
            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new NetMediumException(res.StatusCode);
            }

            var responeBytes = res.Content.ReadAsByteArrayAsync().Result;
            var responseString = Encoding.GetEncoding(1251).GetString(responeBytes);

            if (responseString.IndexOf("Ошибка авторизации. Неправильное имя пользователя или пароль.") != -1)
            {
                throw new LoginMediumException();
            }

            logged = true;
        }

        private HttpContent prepareLoginFormData(string username, string password)
        {
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("login", username));
            formData.Add(new KeyValuePair<string, string>("pwd", password));
            return new FormUrlEncodedContent(formData);
        }

        public bool UploadXML(XmlDocument xml)
        {
            byte[] xmlBytes = getXmlBytes(xml);

            var fileData = new MultipartFormDataContent();
            var fileByteArrayContent = new ByteArrayContent(xmlBytes);
            fileByteArrayContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/xml");

            fileData.Add(fileByteArrayContent, "xml_file", "packagexml.xml");

            var response = _webClient.PostAsync(_hostUrl + "shop/?content=editor&action=loadfromfile&parent=0", fileData).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new NetMediumException(response.StatusCode);
            }

            return true;
        }

        private byte[] getXmlBytes(XmlDocument xmlDoc)
        {
            var stream = new MemoryStream();
            xmlDoc.Save(stream);

            int bytesLength = (int)stream.Length;
            byte[] xmlBytes = new byte[bytesLength];
            
            stream.Position = 0;
            stream.Read(xmlBytes, 0, bytesLength);
            return xmlBytes;
        }

        public static XmlDocument GetPartOfXml(string xmlFileName, int index, int count = 1)
        {
            if (count < 0) return null;

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);

            var rowDataElements = xmlDoc.DocumentElement.GetElementsByTagName("ROWDATA");

            if (rowDataElements.Count == 1)
            {
                var rowDataElement = rowDataElements[0];

                var rowCount = rowDataElement.ChildNodes.Count;

                if (index >= rowCount) return null;

                int t = 0;

                for (int i = 0; i < rowCount; ++i)
                {
                    if (i >= index && i < index + count) continue;

                    t++;
                    if (i < index)
                        rowDataElement.RemoveChild(rowDataElement.ChildNodes[0]);
                    else
                        rowDataElement.RemoveChild(rowDataElement.ChildNodes[count]);
                }

                var a = 0;

                return xmlDoc;
            }
            else
                return null;
        }

        public static XmlDocument GetXml(string xmlFileName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            return xmlDoc;
        }

        public static List<string> GetPublicItems(string fileName, int start, int count)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            var rowDataElements = xmlDoc.DocumentElement.GetElementsByTagName("ROWDATA");

            if (rowDataElements.Count != 1)
                throw new FormatException("Формат XML не соответствует ожидаемому.");

            var rowDataElement = rowDataElements[0];

            var rowCount = rowDataElement.ChildNodes.Count;

            List<string> list = new List<string>();

            for (int i = start; i < rowCount && i < start + count; ++i)
            {
                if (rowDataElement.ChildNodes[i].Attributes["RLT_MAIN_FORINTERNET"].Value == "+")
                {
                    list.Add(rowDataElement.ChildNodes[i].Attributes["RLT_MAIN_ID"].Value);
                }
            }

            return list;       
        }
    }
}
