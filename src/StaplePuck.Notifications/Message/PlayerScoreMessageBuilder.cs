using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Models;
using StaplePuck.Core.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaplePuck.Notifications.Message
{
    public class PlayerScoreMessageBuilder
    {
        public string BuildMessage(FantasyTeamPlayers player, PlayerScoreUpdated playerScoreUpdated, IEnumerable<ScoringType> scoringTypes)
        {
            var types = new List<string>();
            foreach (var scoreType in playerScoreUpdated.ScoringTypesUpdated)
            {
                if (scoreType.CurrentScore > scoreType.OldScore)
                {
                    var typeInfo = scoringTypes.FirstOrDefault(x => x.Id == scoreType.ScoreTypeId);
                    if (typeInfo != null)
                    {
                        types.Add(typeInfo.Name);
                    }
                }
            }
            var scoreList = string.Join(", ", types);
            if (!string.IsNullOrEmpty(scoreList))
            {
                return $"{player?.Player?.FullName} awarded: {scoreList}";
            }
            else
            {
                return string.Empty;
                // log warning
            }
        }
    }
}
