using LuxImperium.Models;
using Xunit;

namespace LuxImperium.Test.Models
{
    public class DmxChannelTest
    {
        [Fact]
        public void DmxChannel_Properties_Should_Set_And_Get_Values()
        {
            // Arrange
            var expectedChannelNumber = 1;
            var expectedValue = 255;
            var expectedName = "Test channel";

            var dmxChannel = new DmxChannel
            {
                // Act
                Number = expectedChannelNumber,
                Name = expectedName
            };

            dmxChannel.InitialValue = expectedValue;

            // Assert
            Assert.Equal(expectedChannelNumber, dmxChannel.Number);
            Assert.Equal(expectedValue, dmxChannel.InitialValue);
            Assert.Equal(expectedName, dmxChannel.Name);
        }
    }
}