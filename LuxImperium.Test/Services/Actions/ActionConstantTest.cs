using Moq;
using Xunit;
using System.Collections.Generic;
using LuxImperium.Models;
using LuxImperium.Services;
using LuxImperium.Services.Actions;

namespace LuxImperium.Test.Services.Actions
{
    public class ActionConstantTests
    {
        [Fact]
        public void Execute_Should_Return_Correct_Results()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = "1,2,3", Fixture = "fixture name"};
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int> { 1, 2, 3 });

            var actionConstant = new ActionConstant(mockParseChannelProperty.Object, dmxAction);

            var results = actionConstant.Execute();

            Assert.Equal(3, results.Count);
            Assert.Equal(1, results[0].Channel);
            Assert.Equal(2, results[1].Channel);
            Assert.Equal(3, results[2].Channel);
        }

        [Fact]
        public void Execute_Should_Handle_Empty_Channels()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = "" };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int>());

            var actionConstant = new ActionConstant(mockParseChannelProperty.Object, dmxAction);

            var results = actionConstant.Execute();

            Assert.Empty(results);
        }

        [Fact]
        public void Execute_Should_Handle_Null_Channels()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = null };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int>());

            var actionConstant = new ActionConstant(mockParseChannelProperty.Object, dmxAction);

            var results = actionConstant.Execute();

            Assert.Empty(results);
        }
    }
}