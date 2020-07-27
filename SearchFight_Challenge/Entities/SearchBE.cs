using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight_Challenge.Entities
{
    public class SearchBE
    {
        public string searchEngine { get; set; }
        public string query { get; set; }
        public double resultCount { get; set; }
        public double max { get; set; }

    }
}
