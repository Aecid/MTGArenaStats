namespace GetArenaStats.Contracts
{
    public class Stats
    {
        public string playerId { get; set; }
        public string classHighWaterMark { get; set; }
        public int tier { get; set; }
        public int progress { get; set; }
        public int streak { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
    }
}
