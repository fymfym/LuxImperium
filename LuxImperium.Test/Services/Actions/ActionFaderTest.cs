using Moq;
using Xunit;
using System.Collections.Generic;
using LuxImperium.Models;
using LuxImperium.Services;
using LuxImperium.Services.Actions;

namespace LuxImperium.Test.Services.Actions
{
    public class ActionFaderTests
    {
        [Fact]
        public void Execute_Should_Return_Correct_Results_On_Interval()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = "1,2,3", ActionValues = new Dictionary<string, string> { { "step", "10" }, { "interval", "1" }, { "startValue", "0" } } };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int> { 1, 2, 3 });

            var actionFader = new ActionFader(mockParseChannelProperty.Object, dmxAction);

            var results = actionFader.Execute();

            Assert.Equal(3, results.Count);
            Assert.All(results, result => Assert.Equal(10, result.Value));
        }

        [Fact]
        public void Execute_Should_Reset_Value_If_Exceeds_255()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = "1,2,3", ActionValues = new Dictionary<string, string> { { "step", "10" }, { "interval", "1" }, { "startValue", "250" } } };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int> { 1, 2, 3 });

            var actionFader = new ActionFader(mockParseChannelProperty.Object, dmxAction);

            actionFader.Execute(); // First execution
            var results = actionFader.Execute(); // Second execution

            Assert.Equal(3, results.Count);
            Assert.All(results, result => Assert.Equal(250, result.Value));
        }

        [Fact]
        public void Execute_Should_Handle_Empty_Channels()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = "", ActionValues = new Dictionary<string, string> { { "step", "10" }, { "interval", "1" }, { "startValue", "0" } } };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int>());

            var actionFader = new ActionFader(mockParseChannelProperty.Object, dmxAction);

            var results = actionFader.Execute();

            Assert.Empty(results);
        }

        [Fact]
        public void Execute_Should_Handle_Null_Channels()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = null, ActionValues = new Dictionary<string, string> { { "step", "10" }, { "interval", "1" }, { "startValue", "0" } } };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int>());

            var actionFader = new ActionFader(mockParseChannelProperty.Object, dmxAction);

            var results = actionFader.Execute();

            Assert.Empty(results);
        }
    }
}