using GetArenaStats.Contracts;
using System;
using System.IO;
using System.Linq;

namespace GetArenaStats
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = new DirectoryInfo(System.AppContext.BaseDirectory);
            //directory = new DirectoryInfo(@"C:\Users\Ace\Documents\MTGA\Logs");

            var logs = directory.GetFiles("*.htm").ToList();
            if (logs.Count() == 0)
            {
                Console.WriteLine("No htm files found");
                return;
            }

            AllStats stats;

            while (logs.Count() > 0)
            {
                var latestLog = logs.OrderByDescending(f => f.LastWriteTime).First();
                stats = LogParser.GetUserStats(latestLog.FullName);
                if (stats == null)
                {
                    Console.WriteLine("No data.");
                    logs.Remove(latestLog);
                }
                else
                {
                    ShowData(stats);
                    return;
                }
            }
        }

        private static void ShowData(AllStats stats)
        {
            var constructedWins = stats.constructed.wins;
            var constructedLosses = stats.constructed.losses;
            var constructedTotalGames = constructedWins + constructedLosses;
            float constructedWinPercentage = ((float)constructedWins / (float)constructedTotalGames) * 100;

            var limitedWins = stats.limited.wins;
            var limitedLosses = stats.limited.losses;
            var limitedTotalGames = limitedWins + limitedLosses;
            float limitedWinPercentage = ((float)limitedWins / (float)limitedTotalGames) * 100;

            Console.WriteLine("Constructed:");
            Console.WriteLine($"Total games: {constructedTotalGames}");
            Console.WriteLine($"Wins: {constructedWins}");
            Console.WriteLine($"Losses: {constructedLosses}");
            Console.WriteLine($"Win percentage: {constructedWinPercentage.ToString("0.00")}%");
            Console.WriteLine("\r\nLimited:");
            Console.WriteLine($"Total games: {limitedTotalGames}");
            Console.WriteLine($"Wins: {limitedWins}");
            Console.WriteLine($"Losses: {limitedLosses}");
            Console.WriteLine($"Win percentage: {limitedWinPercentage.ToString("0.00")}%");
            Console.ReadLine();
        }
    }
}
