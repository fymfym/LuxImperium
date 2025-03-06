using LuxImperium.Services;
using Xunit;

namespace LuxImperium.Test.Services
{
    public class ChannelValueValidatorTests
    {
        [Fact]
        public void SetValue_Should_Set_Correct_Value()
        {
            var validator = new ChannelValueValidator();
            validator.SetValue(100);
            Assert.Equal(100, validator.Value);
        }

        [Fact]
        public void AddValue_Should_Increase_Value_Correctly()
        {
            var validator = new ChannelValueValidator();
            validator.SetValue(100);
            validator.AddValue(50);
            Assert.Equal(150, validator.Value);
        }

        [Fact]
        public void AddValue_Should_Not_Exceed_Max_Value()
        {
            var validator = new ChannelValueValidator();
            validator.SetValue(200);
            validator.AddValue(100);
            Assert.Equal(255, validator.Value);
        }

        [Fact]
        public void SubtractValue_Should_Decrease_Value_Correctly()
        {
            var validator = new ChannelValueValidator();
            validator.SetValue(100);
            validator.SubtractValue(50);
            Assert.Equal(50, validator.Value);
        }

        [Fact]
        public void SubtractValue_Should_Not_Go_Below_Min_Value()
        {
            var validator = new ChannelValueValidator();
            validator.SetValue(50);
            validator.SubtractValue(100);
            Assert.Equal(0, validator.Value);
        }
    }
}