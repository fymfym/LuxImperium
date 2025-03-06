using Xunit;
using LuxImperium.Models;

namespace LuxImperium.Test.Models
{
    public class ActionExecuteResultTests
    {
        [Fact]
        public void ActionExecuteResult_Channel_Should_Set_And_Get_Value()
        {
            // Arrange
            var actionExecuteResult = new ActionExecuteResult();
            int expectedChannel = 5;

            // Act
            actionExecuteResult.Channel = expectedChannel;
            int actualChannel = actionExecuteResult.Channel;

            // Assert
            Assert.Equal(expectedChannel, actualChannel);
        }

        [Fact]
        public void ActionExecuteResult_Value_Should_Set_And_Get_Value()
        {
            // Arrange
            var actionExecuteResult = new ActionExecuteResult();
            byte expectedValue = 255;

            // Act
            actionExecuteResult.Value = expectedValue;
            byte actualValue = actionExecuteResult.Value;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
