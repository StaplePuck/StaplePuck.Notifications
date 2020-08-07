﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace StaplePuck.Notifications
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = "{\"LeagueId\":4,\"PlayersScoreUpdated\":[{\"PlayerId\":637,\"OldScore\":0,\"CurrentScore\":4,\"ScoringTypesUpdated\":[{\"ScoreTypeId\":1,\"Name\":null,\"OldScore\":0,\"CurrentScore\":4}]},{\"PlayerId\":176,\"OldScore\":0,\"CurrentScore\":2,\"ScoringTypesUpdated\":[{\"ScoreTypeId\":2,\"Name\":null,\"OldScore\":0,\"CurrentScore\":2}]}],\"FantansyTeamChanges\":[{\"FantasyTeamId\":120,\"OldScore\":23,\"CurrentScore\":25,\"OldRank\":8,\"CurrentRank\":7},{\"FantasyTeamId\":121,\"OldScore\":33,\"CurrentScore\":35,\"OldRank\":1,\"CurrentRank\":1},{\"FantasyTeamId\":127,\"OldScore\":21,\"CurrentScore\":23,\"OldRank\":12,\"CurrentRank\":12},{\"FantasyTeamId\":130,\"OldScore\":16,\"CurrentScore\":20,\"OldRank\":27,\"CurrentRank\":16},{\"FantasyTeamId\":137,\"OldScore\":23,\"CurrentScore\":25,\"OldRank\":8,\"CurrentRank\":7},{\"FantasyTeamId\":140,\"OldScore\":21,\"CurrentScore\":25,\"OldRank\":12,\"CurrentRank\":7},{\"FantasyTeamId\":142,\"OldScore\":22,\"CurrentScore\":26,\"OldRank\":10,\"CurrentRank\":6},{\"FantasyTeamId\":146,\"OldScore\":30,\"CurrentScore\":34,\"OldRank\":3,\"CurrentRank\":2},{\"FantasyTeamId\":150,\"OldScore\":30,\"CurrentScore\":32,\"OldRank\":3,\"CurrentRank\":3}]}";

            var handler = MessageHandler.GetHandler();
            var result = handler.ProcessMessage(message).Result;

            //var updater = Updater.Init();
            //updater.Update();
        }
    }
}