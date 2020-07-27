using SearchFight_Challenge.Engine;
using SearchFight_Challenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lQueries = new List<string>();

            if (args.Count() > 0)
            {
                for (int j = 0; j < args.Count(); j++)
                {
                    lQueries.Add(args[j]);
                }

                SearchResult sr = SearchEngine.Search(lQueries);
                ShowResults(sr, lQueries);
            }
            else
            {
                Console.WriteLine("The app need parameters");
            }

            Console.ReadLine();
        }


        static void ShowResults(SearchResult sr, List<string> lQueries)
        {
            lQueries.ForEach(query =>
            {
                StringBuilder sb = new StringBuilder();
                List<SearchBE> lQueryData = sr.TotalData.FindAll(data => data.query == query);

                lQueryData.ForEach(item =>{sb.Append(item.searchEngine).Append(": ").Append(item.resultCount.ToString("##,#")).Append(" \t ");});
                Console.WriteLine(query+ ": \t" + sb);
            }
            );

            Console.WriteLine(Environment.NewLine);
            sr.TotalByEngine.ForEach(winByEng => { Console.WriteLine(winByEng.searchEngine + " Winner: " + winByEng.query); });
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Total Winner :" + sr.TotalWinner);
        }
    }
}
