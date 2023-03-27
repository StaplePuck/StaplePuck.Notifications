using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Models;
using StaplePuck.Core.Stats;

namespace StaplePuck.Notifications
{
    public class MessageBuilder : IMessageBuilder
    {
        public IEnumerable<Message> BuildMessages(ScoreUpdated updated, League league)
        {
            var messages = new List<Message>();
            foreach (var teamUpdated in updated.FantansyTeamChanges)
            {
                var teamInfo = league.FantasyTeams.FirstOrDefault(x => x.Id == teamUpdated.FantasyTeamId);
                if (teamInfo.GM.ReceiveNotifications && teamInfo.GM.NotificationTokens.Count() > 0 && teamInfo.IsPaid)
                {
                    var builder = new StringBuilder();
                    foreach (var player in teamInfo.FantasyTeamPlayers)
                    {
                        var playerUpdated = updated.PlayersScoreUpdated.FirstOrDefault(x => x.PlayerId == player.PlayerId);
                        if (playerUpdated != null)
                        {
                            var types = new List<string>();
                            foreach (var scoreType in playerUpdated.ScoringTypesUpdated)
                            {
                                if (scoreType.CurrentScore > scoreType.OldScore)
                                {
                                    var typeInfo = league.Season.Sport.ScoringTypes.FirstOrDefault(x => x.Id == scoreType.ScoreTypeId);
                                    if (typeInfo != null)
                                    {
                                        types.Add(typeInfo.Name);
                                    }
                                }
                            }
                            var scoreList = string.Join(", ", types);
                            if (!string.IsNullOrEmpty(scoreList))
                            {
                                builder.AppendLine($"{player.Player.FullName} awarded: {scoreList}");
                            }
                            else
                            {
                                // log warning
                            }
                        }
                    }
                    if (teamUpdated.CurrentRank < teamUpdated.OldRank)
                    {
                        builder.AppendLine($"Good news! Your team has gone from {teamUpdated.OldRank} to {teamUpdated.CurrentRank}");
                    }
                    else if (teamUpdated.CurrentRank > teamUpdated.OldRank && teamInfo.GM.ReceiveNegativeNotifications)
                    {
                        builder.AppendLine($"Uh oh! Another team has moved ahead of you in the standings. Your team has gone from {teamUpdated.OldRank} to {teamUpdated.CurrentRank}");
                    }
                    var text = builder.ToString();
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        var notification = new Notification
                        {
                            title = teamInfo.Name,
                            text = text
                        };
                        var message = new Message
                        {
                            notification = notification,
                            registration_ids = teamInfo.GM.NotificationTokens.Select(x => x.Token).Distinct().ToArray()
                        };
                        messages.Add(message);
                    }
                }
            }
            return messages;
        }
    }
}
