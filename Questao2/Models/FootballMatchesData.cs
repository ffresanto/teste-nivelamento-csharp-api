namespace Questao2.Models
{
    public class FootballMatchesData
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<FootballMatch> data { get; set; }

    }
}
