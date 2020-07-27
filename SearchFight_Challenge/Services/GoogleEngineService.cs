using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SearchFight_Challenge.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight_Challenge.Services
{
    public class GoogleEngineService : ISearchEngine
    {
        public SearchBE Search(string query)
        {
            SearchBE r = new SearchBE();

            try
            {
                HttpClient GoogleSearch = new HttpClient()
                {
                    BaseAddress = new Uri("https://www.googleapis.com/")
                };

                JObject result = new JObject();
                string key = ConfigurationManager.AppSettings.Get("GoogleAPIKey");
                string cx = ConfigurationManager.AppSettings.Get("GoogleAPIcx");
                
                var response = GoogleSearch.GetAsync("customsearch/v1?key=" + key + "&cx="+cx+"&q=" + query).Result;

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<JObject>(jsonString);
                    if (result != null)
                    {
                        r.searchEngine = "Google";
                        r.query = query;
                        r.resultCount = long.Parse(result["queries"]["request"][0]["totalResults"].ToString());
                    }
                }
                else
                {
                    r.searchEngine = "Google";
                    r.query = query;
                    r.resultCount = 0;
                }
            }
            catch (Exception e)
            {
                r.searchEngine = "Google";
                r.query = query + e.Message;
                r.resultCount = 0;
            }

            return r;
        }
    }
}
