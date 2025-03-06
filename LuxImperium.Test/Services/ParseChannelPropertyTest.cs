using Xunit;
using System.Collections.Generic;
using LuxImperium.Services;

namespace LuxImperium.Test.Services
{
    public class ParseChannelPropertyTests
    {
        [Fact]
        public void Parse_Should_Return_All_Channels_For_All_Text()
        {
            var parser = new ParseChannelProperty();
            var result = parser.Parse("all");
            Assert.Equal(512, result.Count);
            Assert.Equal(1, result[0]);
            Assert.Equal(512, result[511]);
        }

        [Fact]
        public void Parse_Should_Return_Correct_Channels_For_Comma_Separated_Text()
        {
            var parser = new ParseChannelProperty();
            var result = parser.Parse("1,2,3");
            Assert.Equal(new List<int> { 1, 2, 3 }, result);
        }

        [Fact]
        public void Parse_Should_Handle_Empty_String()
        {
            var parser = new ParseChannelProperty();
            var result = parser.Parse("");
            Assert.Empty(result);
        }

        [Fact]
        public void Parse_Should_Handle_Null_String()
        {
            var parser = new ParseChannelProperty();
            var result = parser.Parse(null);
            Assert.Empty(result);
        }

        [Fact]
        public void Parse_Should_Throw_Exception_For_Invalid_Input()
        {
            var parser = new ParseChannelProperty();
            Assert.Throws<System.FormatException>(() => parser.Parse("invalid"));
        }
    }
}