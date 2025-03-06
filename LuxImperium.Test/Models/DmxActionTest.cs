using Xunit;
using System.Collections.Generic;
using LuxImperium.Models;

namespace LuxImperium.Test.Models
{
    public class DmxActionTests
    {
        [Fact]
        public void DmxAction_Properties_Should_Set_And_Get_Values()
        {
            // Arrange
            var expectedFixture = "Fixture1";
            var expectedAction = "Action1";
            var expectedDisabled = true;
            var expectedChannels = "1,2,3";
            var expectedActionValues = new Dictionary<string, string>
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            };

            var dmxAction = new DmxAction();

            // Act
            dmxAction.Fixture = expectedFixture;
            dmxAction.Action = expectedAction;
            dmxAction.Disabled = expectedDisabled;
            dmxAction.Channels = expectedChannels;
            dmxAction.ActionValues = expectedActionValues;

            // Assert
            Assert.Equal(expectedFixture, dmxAction.Fixture);
            Assert.Equal(expectedAction, dmxAction.Action);
            Assert.Equal(expectedDisabled, dmxAction.Disabled);
            Assert.Equal(expectedChannels, dmxAction.Channels);
            Assert.Equal(expectedActionValues, dmxAction.ActionValues);
        }
    }
}