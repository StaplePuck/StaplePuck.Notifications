using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaplePuck.Notifications.Message
{
    public class FantasyTeamStateMessageBuilder : IFantasyTeamStateMessageBuilder
    {
        public string BuildMessage(FantasyTeam fantasyTeam, FantasyTeamChanged fantasyTeamChanged)
        {
            var message = string.Empty;
            if (fantasyTeamChanged.CurrentRank < fantasyTeamChanged.OldRank)
            {
                message = $"Good news! Your team has gone from {fantasyTeamChanged.OldRank} to {fantasyTeamChanged.CurrentRank}";
            }
            else if (fantasyTeamChanged.CurrentRank > fantasyTeamChanged.OldRank && fantasyTeam.GM!.ReceiveNegativeNotifications)
            {
                message = $"Uh oh! Another team has moved ahead of you in the standings. Your team has gone from {fantasyTeamChanged.OldRank} to {fantasyTeamChanged.CurrentRank}";
            }
            return message;
        }
    }
}
