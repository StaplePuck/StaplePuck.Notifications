using FirebaseAdmin.Messaging;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Models;
using StaplePuck.Core.Stats;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaplePuck.Notifications.Message
{
    public class FantasyTeamMessageBuilder
    {
        private readonly IFantasyTeamStateMessageBuilder _fantasyTeamStateMessageBuilder;
        public FantasyTeamMessageBuilder(IFantasyTeamStateMessageBuilder fantasyTeamStateMessageBuilder) 
        {
            _fantasyTeamStateMessageBuilder = fantasyTeamStateMessageBuilder;
        }

        public NotificationMessage? BuildMessage(FantasyTeam fantasyTeam, FantasyTeamChanged fantasyTeamChanged, IEnumerable<PlayerScoreUpdated> playerScoreUpdated, IEnumerable<ScoringType> scoringTypes)
        {
            if (fantasyTeam?.GM != null && fantasyTeam.GM.ReceiveNotifications && fantasyTeam.GM.NotificationTokens.Count() > 0 && fantasyTeam.FantasyTeamPlayers != null && fantasyTeam.IsPaid)
            {
                var builder = new StringBuilder();
                foreach (var player in fantasyTeam.FantasyTeamPlayers)
                {
                    var playerUpdated = playerScoreUpdated.FirstOrDefault(x => x.PlayerId == player.PlayerId);
                    if (playerUpdated != null)
                    {
                        var types = new List<string>();
                        foreach (var scoreType in playerUpdated.ScoringTypesUpdated)
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
                            builder.AppendLine($"{player?.Player?.FullName} awarded: {scoreList}");
                        }
                        else
                        {
                            // log warning
                        }
                    }
                }
                builder.AppendLine(_fantasyTeamStateMessageBuilder.BuildMessage(fantasyTeam, fantasyTeamChanged));
                
                var text = builder.ToString();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    var notification = new Notification
                    {
                        title = fantasyTeam.Name,
                        text = text
                    };
                    var message = new NotificationMessage
                    {
                        notification = notification,
                        registration_ids = fantasyTeam.GM.NotificationTokens.Select(x => x.Token).Distinct().ToArray()
                    };
                    return message;
                }
            }
            return null;
        }
    }
}
