using Newtonsoft.Json.Linq;
using SearchFight_Challenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight_Challenge.Services
{
    public interface ISearchEngine
    {
        SearchBE Search(string word);
    }
}
