using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Models;

namespace StaplePuck.Notifications.Message
{
    public interface IFantasyTeamStateMessageBuilder
    {
        string BuildMessage(FantasyTeam fantasyTeam, FantasyTeamChanged fantasyTeamChanged);
    }
}