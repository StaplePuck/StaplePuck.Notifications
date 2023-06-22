using StaplePuck.Notifications.Message;

namespace StaplePuck.Notifications.Firebase;

public interface IFCMClient
{
    Task<bool> SendNotification(NotificationMessage message);
}
