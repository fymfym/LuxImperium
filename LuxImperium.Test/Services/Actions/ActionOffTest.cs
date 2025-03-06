using Moq;
using Xunit;
using System.Collections.Generic;
using LuxImperium.Models;
using LuxImperium.Services;
using LuxImperium.Services.Actions;

namespace LuxImperium.Test.Services.Actions
{
    public class ActionOffTests
    {
        [Fact]
        public void Execute_Should_Set_All_Channels_To_Zero()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = "1,2,3" };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int> { 1, 2, 3 });

            var actionOff = new ActionOff(mockParseChannelProperty.Object, dmxAction);

            var results = actionOff.Execute();

            Assert.Equal(3, results.Count);
            Assert.All(results, result => Assert.Equal(0, result.Value));
        }

        [Fact]
        public void Execute_Should_Handle_Empty_Channels()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = "" };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int>());

            var actionOff = new ActionOff(mockParseChannelProperty.Object, dmxAction);

            var results = actionOff.Execute();

            Assert.Empty(results);
        }

        [Fact]
        public void Execute_Should_Handle_Null_Channels()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = null };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int>());

            var actionOff = new ActionOff(mockParseChannelProperty.Object, dmxAction);

            var results = actionOff.Execute();

            Assert.Empty(results);
        }

        [Fact]
        public void Execute_Should_Handle_Single_Channel()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = "1" };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int> { 1 });

            var actionOff = new ActionOff(mockParseChannelProperty.Object, dmxAction);

            var results = actionOff.Execute();

            Assert.Single(results);
            Assert.Equal(0, results[0].Value);
        }
    }
}
