using StaplePuck.Core.Fantasy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaplePuck.Notifications
{
    public interface IScoreProvider
    {
        Task<League> GetLeagueScores(int leagueId);
    }
}
