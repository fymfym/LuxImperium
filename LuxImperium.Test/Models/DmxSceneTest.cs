using System.Collections.Generic;
using LuxImperium.Models;
using Xunit;

namespace LuxImperium.Test.Models
{
    public class DmxSceneTest
    {
        [Fact]
        public void DmxScene_Properties_Should_Set_And_Get_Values()
        {
            // Arrange
            var expectedName = "Scene1";
            var expectedActions = new List<DmxAction>
            {
                new DmxAction { Fixture = "Fixture1", Action = "Action1" },
                new DmxAction { Fixture = "Fixture2", Action = "Action2" }
            };

            var expectedFixtures = new List<DmxFixture>
            {
                new DmxFixture { Name = "name1", Channels = new List<DmxChannel> { new DmxChannel { Number = 1, InitialValue = 255 } } },
            };

            var startAction = new DmxAction();
            var stopAction = new DmxAction();
            
            var dmxScene = new DmxScene
            {
                // Act
                Name = expectedName,
                Actions = expectedActions,
                Fixtures = expectedFixtures,
                StartAction = startAction,
                StopAction = stopAction
            };

            // Assert
            Assert.Equal(expectedName, dmxScene.Name);
            Assert.Equal(expectedActions, dmxScene.Actions);
            Assert.Equal(expectedFixtures, dmxScene.Fixtures);
            Assert.Equal(startAction, dmxScene.StartAction);
            Assert.Equal(stopAction, dmxScene.StopAction);
        }
    }
}