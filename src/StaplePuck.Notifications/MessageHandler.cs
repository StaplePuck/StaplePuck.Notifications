using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StaplePuck.Core.Auth;
using StaplePuck.Core.Client;
using StaplePuck.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaplePuck.Notifications
{
    public class MessageHandler
    {
        public static MessageHandler GetHandler()
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddOptions()
                .AddSingleton<IMessageBuilder, MessageBuilder>()
                .AddSingleton<IScoreProvider, ScoreProvider>()
                .AddSingleton<IFCMClient, FCMClient>()
                .AddSingleton<MessageHandler>()
                .AddAuth0Client(configuration)
                .AddStaplePuckClient(configuration)
                .BuildServiceProvider();

            return serviceProvider.GetService<MessageHandler>();
        }

        private readonly IMessageBuilder _messageBuilder;
        private readonly IScoreProvider _scoreProvider;
        private readonly IFCMClient _fCMClient;

        public MessageHandler(IMessageBuilder messageBuilder, IScoreProvider scoreProvider, IFCMClient fCMClient)
        {
            _messageBuilder = messageBuilder;
            _scoreProvider = scoreProvider;
            _fCMClient = fCMClient;
        }

        public async Task<bool> ProcessMessage(string message)
        {
            var update = JsonConvert.DeserializeObject<ScoreUpdated>(message);
            if (update.PlayersScoreUpdated == null)
            {
                Console.Out.WriteLine($"Warning: message is in wrong format. {message}");
                return true;
            }

            if (update.PlayersScoreUpdated.Count() == 0 && update.FantansyTeamChanges.Count() == 0)
            {
                return true;
            }
            var league = await _scoreProvider.GetLeagueScores(update.LeagueId);

            var messages = _messageBuilder.BuildMessages(update, league);
            foreach (var item in messages)
            {
                await _fCMClient.SendNotification(item);
            }

            return true;
        }
    }
}
