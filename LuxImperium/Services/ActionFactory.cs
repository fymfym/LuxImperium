using System;
using LuxImperium.Models;
using LuxImperium.Services.Actions;

namespace LuxImperium.Services
{
    public class ActionFactory : IActionFactory
    {
        public IAction Build(DmxAction action)
        {
            if (action.Disabled)
            {
                return null;
            }

            IParseChannelProperty channelPropertyParser = new ParseChannelProperty();
            
            switch (action.Action.ToLower())
            {
                case "fader":
                    return new ActionFader(channelPropertyParser, action);
                case "constant":
                    return new ActionConstant(channelPropertyParser, action);
                case "onoff":
                    return new ActionOnOff(channelPropertyParser, action);
                case "off":
                    return new ActionOff(channelPropertyParser, action);
                default:
                    throw new ArgumentException($"Action <{action.Action}> not implemented in ActionFactory");
            }
        }
    }
}