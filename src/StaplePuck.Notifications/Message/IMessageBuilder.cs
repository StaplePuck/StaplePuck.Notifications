using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Models;

namespace StaplePuck.Notifications.Message;

public interface IMessageBuilder
{
    IEnumerable<NotificationMessage> BuildMessages(ScoreUpdated updated, League league);
}
