using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SearchFight_Challenge.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight_Challenge.Services
{
    public class BingEngineService : ISearchEngine
    {

        public SearchBE Search(string query)
        {
            SearchBE r = new SearchBE();
            try
            {
                string uriBase = ConfigurationManager.AppSettings.Get("BINGUriBase");
                string uriQuery = uriBase + "?q=" + Uri.EscapeDataString(query);

                WebRequest request = WebRequest.Create(uriQuery);
                request.Headers["Ocp-Apim-Subscription-Key"] = ConfigurationManager.AppSettings.Get("BINGAPIKey");
                HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
                string jsonString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                JObject result = JsonConvert.DeserializeObject<JObject>(jsonString);

                if (result != null)
                {
                    r.searchEngine   = "BING";
                    r.query  = query;
                    r.resultCount = long.Parse(result["webPages"]["totalEstimatedMatches"].ToString());
                }
                else
                {
                    r.searchEngine   = "BING";
                    r.query = query;
                    r.resultCount = 0;
                }
            }
            catch (Exception e)
            {
                r.searchEngine = "BING";
                r.query = query + e.Message;
                r.resultCount = 0;
            }

            return r;

        }
    }
}
