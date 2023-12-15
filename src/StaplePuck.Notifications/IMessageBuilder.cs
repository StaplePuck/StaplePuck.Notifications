using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Notifications
{
    public interface IMessageBuilder
    {
        IEnumerable<Message> BuildMessages(ScoreUpdated updated, League league);
    }
}
