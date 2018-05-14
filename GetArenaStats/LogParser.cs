using GetArenaStats.Contracts;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace GetArenaStats
{
    public static class LogParser
    {
        public static AllStats GetUserStats(string fileName)
        {
            Console.WriteLine($"Parsing {fileName}");
            Regex regex = new Regex(@"Event.GetCombinedRankInfo.*(\{(.|\s)*\})", RegexOptions.Multiline);
            string block = "";

            using (var sr = new StreamReader(fileName, Encoding.UTF8, true, 1024))
            {
                int counter = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("Response") && line.Contains("Event.GetCombinedRankInfo"))
                    {
                        block += "{";
                        counter++;
                    }

                    if (counter > 1 && counter <= 25)
                    {
                        block += line;
                        counter++;
                    }

                    if (counter > 25)
                    {
                        break;
                    }
                }
            }

            var json = regex.Match(block).ToString().Replace("Event.GetCombinedRankInfo ", "");
            var resultObject = JsonConvert.DeserializeObject<AllStats>(json);

            return resultObject;
        }
    }
}
