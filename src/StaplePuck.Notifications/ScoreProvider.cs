using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using StaplePuck.Core.Client;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.Notifications
{
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

        public async Task<League> GetLeagueScores(int leagueId)
        {
            var variables = new ExpandoObject() as IDictionary<string, object>;
            variables.Add("leagueId", leagueId.ToString());

            var result = await _staplePuckClient.GetAsync<League>(LEAGUE_QUEURY, variables);

            if (result.Length == 0)
            {
                return null;
            }

            var league = result[0];
            return league;
        }
    }
}
