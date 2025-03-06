using Xunit;
using System;
using System.Collections.Generic;
using LuxImperium.Models;
using LuxImperium.Services;
using LuxImperium.Services.Actions;

namespace LuxImperium.Test.Services
{
    public class ActionFactoryTests
    {
        [Fact]
        public void Build_Should_Return_Null_If_Action_Is_Disabled()
        {
            var action = new DmxAction { Disabled = true };
            var factory = new ActionFactory();

            var result = factory.Build(action);

            Assert.Null(result);
        }

        [Fact]
        public void Build_Should_Return_ActionFader_For_Fader_Action()
        {
            var action = CreateDmxAction("fader");
            var factory = new ActionFactory();

            var result = factory.Build(action);

            Assert.IsType<ActionFader>(result);
        }

        [Fact]
        public void Build_Should_Return_ActionConstant_For_Constant_Action()
        {
            var action = CreateDmxAction("constant");
            var factory = new ActionFactory();

            var result = factory.Build(action);

            Assert.IsType<ActionConstant>(result);
        }

        [Fact]
        public void Build_Should_Return_ActionOnOff_For_OnOff_Action()
        {
            var action = CreateDmxAction("onoff");
            var factory = new ActionFactory();

            var result = factory.Build(action);

            Assert.IsType<ActionOnOff>(result);
        }

        [Fact]
        public void Build_Should_Return_ActionOff_For_Off_Action()
        {
            var action = CreateDmxAction("off");
            var factory = new ActionFactory();

            var result = factory.Build(action);

            Assert.IsType<ActionOff>(result);
        }

        [Fact]
        public void Build_Should_Throw_Exception_For_Unknown_Action()
        {
            var action = CreateDmxAction("unknown");
            var factory = new ActionFactory();

            Assert.Throws<ArgumentException>(() => factory.Build(action));
        }

        private DmxAction CreateDmxAction(string action)
        {
            return new DmxAction { Action = action, ActionValues = new Dictionary<string, string>(), Channels = "1"};
        }
    }
}