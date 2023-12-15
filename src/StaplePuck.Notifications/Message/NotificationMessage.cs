namespace StaplePuck.Notifications.Message;

public class NotificationMessage
{
    public string[] registration_ids { get; set; } = Array.Empty<string>();
    public Notification notification { get; set; } = new Notification();
    public object data { get; set; } = new object();
}
