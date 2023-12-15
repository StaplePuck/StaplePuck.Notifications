using StaplePuck.Core.Fantasy;

namespace StaplePuck.Notifications.Score;

public interface IScoreProvider
{
    Task<League?> GetLeagueScores(int leagueId);
}
