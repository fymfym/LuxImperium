using System.Collections.Generic;
using LuxImperium.Models;

namespace LuxImperium.Services.Actions
{
    public class ActionOff : IAction
    {
        public ActionOff(
            IParseChannelProperty parseChannelProperty,
            DmxAction dmxAction)
        {
            Channels = parseChannelProperty.Parse(dmxAction.Channels);
        }
        
        public List<int> Channels { get; set; }
        
        public List<ActionExecuteResult> Execute()
        {
            var result = new List<ActionExecuteResult>();

            foreach (var channel in Channels)
            {
                result.Add(new ActionExecuteResult()
                {
                    Channel = channel,
                    Value = 0
                });
            }

            return result;
        }
    }
}