using Questao2.Models;
using System.Net;
using System.Text.Json;

namespace Questao2.Services
{
    public class FootballService
    {
        private const string apiUrl = "https://jsonmock.hackerrank.com/api/football_matches";

        public int GetTotalScoredGoals(string teamName, int year, int team)
        {
            try
            {

                FootballMatchesData matchesData = FetchFootballMatches(teamName, year, team);
                int page = matchesData.page;
                int totalGoals = 0;

                while (page <= matchesData.total_pages)
                {
                    foreach (FootballMatch match in matchesData.data)
                        totalGoals += int.Parse(team == 1 ? match.team1goals : match.team2goals);

                    page++;

                    matchesData = FetchFootballMatches(teamName, year, team, page);
                }

                return totalGoals;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private FootballMatchesData FetchFootballMatches(string teamName, int year, int team, int? page = null)
        {
            string queryParams = $"?year={year}&team{team}={teamName}&page={page}";
            string fullUrl = apiUrl + queryParams;
            string json;

            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync(fullUrl).GetAwaiter().GetResult();

                if (result.StatusCode != HttpStatusCode.OK)
                    throw new Exception();

                json = (result).Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<FootballMatchesData>(json)!;
            }
        }
    }
}
