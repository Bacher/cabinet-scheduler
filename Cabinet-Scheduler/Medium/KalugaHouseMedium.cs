using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Medium
{
    public class KalugaHouseMedium
    {
        private HttpClient _webClient = new HttpClient();
        private string _hostUrl;
        public bool logged = false;

        private static readonly string _searchPattern = @"<td[^>]*><a href=""/item/(\d+)/"">\1</a></td>\s*<td[^>]*>{0}</td>";

        public KalugaHouseMedium(string url)
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

            if (responseString.IndexOf("Неправильное имя пользователя или пароль.") != -1)
            {
                throw new LoginMediumException();
            }

            logged = true;
        }

        private HttpContent prepareLoginFormData(string username, string password)
        {
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("email", username));
            formData.Add(new KeyValuePair<string, string>("password", password));
            return new FormUrlEncodedContent(formData);
        }

        public bool RemoveItemBySecondId(string id)
        {
            if (!logged)
            {
                throw new NotLoggedMediumException();
            }

            var searchResponse = _webClient.GetAsync(_hostUrl + @"?keywords=" + id).Result;

            if (searchResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new NetMediumException(searchResponse.StatusCode);
            }

            var searchResponseBytes = searchResponse.Content.ReadAsByteArrayAsync().Result;
            var searchResponseString = Encoding.GetEncoding(1251).GetString(searchResponseBytes);

            var posOfStartTable = searchResponseString.IndexOf(@"<table class=""user-items"">");
            if (posOfStartTable == -1)
                return false;
            searchResponseString = searchResponseString.Substring(posOfStartTable);

            var posOfEndTable = searchResponseString.IndexOf(@"</table>");
            if(posOfStartTable > 0)
                searchResponseString = searchResponseString.Substring(0, posOfEndTable);


            var matches = new Regex(string.Format(_searchPattern, id)).Match(searchResponseString);

            if (matches.Length == 1)
            {
                return false;
            }

            string _id = matches.Groups[1].Value;

            var res = _webClient.GetAsync(_hostUrl + @"?content=itemdel&item=" + _id).Result;

            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new NetMediumException(res.StatusCode);
            }

            return true;
        }

        public bool CheckItemBySecondId(string id)
        {
            if (!logged)
            {
                throw new NotLoggedMediumException();
            }

            var searchResponse = _webClient.GetAsync(_hostUrl + @"?keywords=" + id).Result;

            if (searchResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new NetMediumException(searchResponse.StatusCode);
            }

            var searchResponseBytes = searchResponse.Content.ReadAsByteArrayAsync().Result;
            var searchResponseString = Encoding.GetEncoding(1251).GetString(searchResponseBytes);

            var posOfStartTable = searchResponseString.IndexOf(@"<table class=""user-items"">");
            if (posOfStartTable == -1)
                return false;
            searchResponseString = searchResponseString.Substring(posOfStartTable);

            var posOfEndTable = searchResponseString.IndexOf(@"</table>");
            if (posOfStartTable > 0)
                searchResponseString = searchResponseString.Substring(0, posOfEndTable);

            var matches = new Regex(string.Format(_searchPattern, id)).Match(searchResponseString);

            if (matches.Length == 1)
            {
                return false;
            }

            return true;
        }
    }
}
