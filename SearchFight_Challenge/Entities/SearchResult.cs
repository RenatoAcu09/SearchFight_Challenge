using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight_Challenge.Entities
{
    public class SearchResult
    {
        public SearchResult()
        {
            TotalData = new List<SearchBE>();
            TotalByEngine = new List<SearchBE>();
        }
        public List<SearchBE> TotalData { get; set; }
        public List<SearchBE> TotalByEngine { get; set; }
        public string TotalWinner { get; set; }
    }
}
