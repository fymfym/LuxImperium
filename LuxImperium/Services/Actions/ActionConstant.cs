using System.Collections.Generic;
using System.Linq;
using LuxImperium.Models;

namespace LuxImperium.Services.Actions
{
    public class ActionConstant : IAction
    {
        private readonly byte _value;

        public ActionConstant(
            IParseChannelProperty parseChannelProperty,
            DmxAction dmxAction)
        {
            _value = ActionTools.GetByte(dmxAction, "startValue");
            Channels = parseChannelProperty.Parse(dmxAction.Channels);
        }

        public List<int> Channels { get; set; }
    
        public List<ActionExecuteResult> Execute()
        {
            return Channels.Select(channel => new ActionExecuteResult() { Channel = channel, Value = _value }).ToList();
        }
    }
}