using Xunit;
using System;
using System.Collections.Generic;
using LuxImperium.Models;
using LuxImperium.Services;

namespace LuxImperium.Test.Services
{
    public class ActionToolsTests
    {
        [Fact]
        public void GetString_Should_Return_Correct_Value()
        {
            var dmxAction = new DmxAction { ActionValues = new Dictionary<string, string> { { "key", "value" } } };
            var result = ActionTools.GetString(dmxAction, "key");
            Assert.Equal("value", result);
        }

        [Fact]
        public void GetString_Should_Return_Null_If_Key_Not_Found()
        {
            var dmxAction = new DmxAction { ActionValues = new Dictionary<string, string>() };
            var result = ActionTools.GetString(dmxAction, "key");
            Assert.Null(result);
        }

        [Fact]
        public void GetByte_Should_Return_Correct_Value()
        {
            var dmxAction = new DmxAction { ActionValues = new Dictionary<string, string> { { "key", "255" } } };
            var result = ActionTools.GetByte(dmxAction, "key");
            Assert.Equal((byte)255, result);
        }

        [Fact]
        public void GetByte_Should_Return_Zero_If_Key_Not_Found()
        {
            var dmxAction = new DmxAction { ActionValues = new Dictionary<string, string>() };
            var result = ActionTools.GetByte(dmxAction, "key");
            Assert.Equal((byte)0, result);
        }

        [Fact]
        public void GetByte_Should_Throw_If_Action_Is_Null()
        {
            var dmxAction = null as DmxAction;
            Assert.Throws<NullReferenceException>(() => ActionTools.GetByte(dmxAction, "key"));
        }
        
        [Fact]
        public void GetBool_Should_Return_Correct_Value()
        {
            var dmxAction = new DmxAction { ActionValues = new Dictionary<string, string> { { "key", "true" } } };
            var result = ActionTools.GetBool(dmxAction, "key");
            Assert.True(result);
        }

        [Fact]
        public void GetBool_Should_Return_False_If_Key_Not_Found()
        {
            var dmxAction = new DmxAction { ActionValues = new Dictionary<string, string>() };
            var result = ActionTools.GetBool(dmxAction, "key");
            Assert.False(result);
        }

        [Fact]
        public void GetInt_Should_Return_Correct_Value()
        {
            var dmxAction = new DmxAction { ActionValues = new Dictionary<string, string> { { "key", "123" } } };
            var result = ActionTools.GetInt(dmxAction, "key");
            Assert.Equal(123, result);
        }

        [Fact]
        public void GetInt_Should_Return_Zero_If_Key_Not_Found()
        {
            var dmxAction = new DmxAction { ActionValues = new Dictionary<string, string>() };
            var result = ActionTools.GetInt(dmxAction, "key");
            Assert.Equal(0, result);
        }
    }
}