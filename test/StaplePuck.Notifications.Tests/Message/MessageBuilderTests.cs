using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Models;
using StaplePuck.Notifications.Message;

namespace StaplePuck.Notifications.Tests.Message;

public class MessageBuilderTests
{
    [Fact]
    public void BuildMessage_NoUpdates_EmptyList()
    {
        // arrange
        var scoreUpdated = new ScoreUpdated { };
        var league = new League { };
        var messageBuilder = new MessageBuilder();

        // act
        var result = messageBuilder.BuildMessages(scoreUpdated, league);

        // assert
        Assert.Empty(result);
    }

    [Fact]
    public void BuildMessage_TeamInfoNotFound_EmptyList()
    {
        // arrange
        var scoreUpdated = new ScoreUpdated 
        {
            FantasyTeamChanges = new FantasyTeamChanged[] { new FantasyTeamChanged { FantasyTeamId = 1 } }
        };
        var league = new League { };
        var messageBuilder = new MessageBuilder();

        // act
        var result = messageBuilder.BuildMessages(scoreUpdated, league);

        // assert
        Assert.Empty(result);
    }
}
