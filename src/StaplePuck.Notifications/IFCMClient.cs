using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaplePuck.Notifications
{
    public interface IFCMClient
    {
        Task<bool> SendNotification(Message message);
    }
}
