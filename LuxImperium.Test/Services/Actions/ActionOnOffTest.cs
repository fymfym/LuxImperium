using Moq;
using Xunit;
using System.Collections.Generic;
using LuxImperium.Models;
using LuxImperium.Services;
using LuxImperium.Services.Actions;

namespace LuxImperium.Test.Services.Actions
{
    public class ActionOnOffTests
    {
        [Fact]
        public void Execute_Should_Toggle_Value_On_And_Off()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { ActionValues = new Dictionary<string, string> { { "onStep", "1" }, { "offStep", "1" }, { "onValue", "255" }, { "offValue", "0" } } };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int> { 1 });

            var actionOnOff = new ActionOnOff(mockParseChannelProperty.Object, dmxAction);

            var firstExecution = actionOnOff.Execute();
            var secondExecution = actionOnOff.Execute();
            var thirdExecution = actionOnOff.Execute();

            Assert.All(firstExecution, result => Assert.Equal(255, result.Value));
            Assert.All(secondExecution, result => Assert.Equal(0, result.Value));
            Assert.All(thirdExecution, result => Assert.Equal(255, result.Value));
        }

        [Fact]
        public void Execute_Should_Handle_Empty_Channels()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = "", ActionValues = new Dictionary<string, string> { { "onStep", "1" }, { "offStep", "1" }, { "onValue", "255" }, { "offValue", "0" } } };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int>());

            var actionOnOff = new ActionOnOff(mockParseChannelProperty.Object, dmxAction);

            var results = actionOnOff.Execute();

            Assert.Empty(results);
        }

        [Fact]
        public void Execute_Should_Handle_Null_Channels()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = null, ActionValues = new Dictionary<string, string> { { "onStep", "1" }, { "offStep", "1" }, { "onValue", "255" }, { "offValue", "0" } } };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int>());

            var actionOnOff = new ActionOnOff(mockParseChannelProperty.Object, dmxAction);

            var results = actionOnOff.Execute();

            Assert.Empty(results);
        }

        [Fact]
        public void Execute_Should_Respect_OnStep_And_OffStep()
        {
            var mockParseChannelProperty = new Mock<IParseChannelProperty>();
            var dmxAction = new DmxAction { Channels = null, ActionValues = new Dictionary<string, string> { { "onStep", "2" }, { "offStep", "3" }, { "onValue", "255" }, { "offValue", "0" } } };
            mockParseChannelProperty.Setup(p => p.Parse(It.IsAny<string>())).Returns(new List<int> { 1 });

            var actionOnOff = new ActionOnOff(mockParseChannelProperty.Object, dmxAction);

            var firstExecution = actionOnOff.Execute();
            var secondExecution = actionOnOff.Execute();
            var thirdExecution = actionOnOff.Execute();
            var fourthExecution = actionOnOff.Execute();
            var fifthExecution = actionOnOff.Execute();

            Assert.All(firstExecution, result => Assert.Equal(0, result.Value));
            Assert.All(secondExecution, result => Assert.Equal(255, result.Value));
            Assert.All(thirdExecution, result => Assert.Equal(255, result.Value));
            Assert.All(fourthExecution, result => Assert.Equal(255, result.Value));
            Assert.All(fifthExecution, result => Assert.Equal(0, result.Value));
        }
    }
}