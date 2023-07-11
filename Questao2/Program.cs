using Questao2.Services;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = GetTotalScoredGoals(teamName, year);

        PrintResult(teamName, totalGoals, year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = GetTotalScoredGoals(teamName, year);

        PrintResult(teamName, totalGoals, year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    private static void PrintResult(string teamName, int totalGoals, int year)
    {
        Console.WriteLine($"Team {teamName} scored {totalGoals} goals in {year}");
    }

    public static int GetTotalScoredGoals(string team, int year)
    {
        FootballService footballService = new FootballService();

        int getTotalGoalsTeam1 = footballService.GetTotalScoredGoals(team, year, 1);
        int getTotalGoalsTeam2 = footballService.GetTotalScoredGoals(team, year, 2);

        return getTotalGoalsTeam1 + getTotalGoalsTeam2;
    }

}