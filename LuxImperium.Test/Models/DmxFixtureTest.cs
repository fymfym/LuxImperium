using System.Collections.Generic;
using LuxImperium.Models;
using Xunit;

namespace LuxImperium.Test.Models
{
    public class DmxFixtureTest
    {
        [Fact]
        public void DmxFixture_Properties_Should_Set_And_Get_Values()
        {
            // Arrange
            var expectedName = "Fixture1";
            var expectedChannels = new List<DmxChannel> {
            {
                new DmxChannel()
                {
                    Number = 1,
                    Name = "name",
                    InitialValue = 123
                }
            } };

            var dmxFixture = new DmxFixture();

            // Act
            dmxFixture.Name = expectedName;
            dmxFixture.Channels = expectedChannels;

            // Assert
            Assert.Equal(expectedName, dmxFixture.Name);
            Assert.Equal(expectedChannels, dmxFixture.Channels);
        }
    }
}