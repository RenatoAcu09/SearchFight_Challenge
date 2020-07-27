using Newtonsoft.Json.Linq;
using SearchFight_Challenge.Entities;
using SearchFight_Challenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight_Challenge.Engine
{
    public class SearchEngine
    {

        static List<string> SearchEngines()
        {
            List<string> searchEngines = new List<string>();
            searchEngines.Add("Google");
            searchEngines.Add("BING");
            return searchEngines;
        }
        
        static List<SearchBE> GetResultByQuery(string query)
        {
            List<string> searchEng = SearchEngines();
            List<SearchBE> lsearch = new List<SearchBE>();

            searchEng.ForEach(se =>
            {
                if (se == "Google") { lsearch.Add(new GoogleEngineService().Search(query)); }
                if (se == "BING") { lsearch.Add(new BingEngineService().Search(query)); }
            });

            return lsearch;
        }

        public static SearchResult Search(List<string> queries)
        {
            SearchResult sr = new SearchResult();

            double totalMax = 0;
            string totalMaxQuery = string.Empty;
            queries.ForEach(query => { sr.TotalData.AddRange(GetResultByQuery(query)); });

            //total max
            sr.TotalData.ForEach(data => { if (data.resultCount > totalMax) { totalMax = data.resultCount; totalMaxQuery = data.query; } });
            sr.TotalWinner = totalMaxQuery;

            //engine max
            SearchEngines().ForEach(engine =>
            {
                double max = 0;
                string maxquery = string.Empty;
               
                List<SearchBE> lByEng = sr.TotalData.FindAll(td => (td.searchEngine == engine));
                lByEng.ForEach(item => { if (item.resultCount > max) { max = item.resultCount; maxquery = item.query; } });
                
                sr.TotalByEngine.Add(new SearchBE() { max = max, query = maxquery, searchEngine = engine });
            });

            return sr;
        }
    }
}
