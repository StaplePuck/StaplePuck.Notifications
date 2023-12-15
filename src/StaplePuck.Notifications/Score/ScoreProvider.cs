using StaplePuck.Core.Client;
using StaplePuck.Core.Fantasy;
using System.Dynamic;

namespace StaplePuck.Notifications.Score;

public class ScoreProvider : IScoreProvider
{
    private readonly IStaplePuckClient _staplePuckClient;
    private const string LEAGUE_QUEURY = @"query league($leagueId: ID) {
  leagues (id: $leagueId) {
    id
    season {
      sport {
        scoringTypes {
          id
          name
        }
      }
    }
    fantasyTeams {
      id
      name
      isPaid
      gM {
        name
        receiveNotifications
        receiveNegativeNotifications
        notificationTokens {
          token
        }
      }
      fantasyTeamPlayers {
        playerId
        player {
          fullName
        }
      }
    }
  }
}";

    public ScoreProvider(IStaplePuckClient client)
    {
        _staplePuckClient = client;
    }

    public async Task<League?> GetLeagueScores(int leagueId)
    {
        var variables = new ExpandoObject() as IDictionary<string, object>;
        variables.Add("leagueId", leagueId);

        var result = await _staplePuckClient.GetAsync<LeagueResponse>(LEAGUE_QUEURY, variables);

        return result?.Leagues.FirstOrDefault();
    }
}
