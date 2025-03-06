using System;
using System.Collections.Generic;
using LuxImperium.Models;

namespace LuxImperium.Services.Actions
{
    public class ActionFader : IAction
    {
        private readonly int _step;
        private readonly byte _interval;
        private readonly byte _startValue;
        private byte _value;

        private int _stepCount;
        
        public ActionFader(
            IParseChannelProperty parseChannelProperty,
            DmxAction dmxAction)
        {
            _step = ActionTools.GetInt(dmxAction,"step");
            _interval = ActionTools.GetByte(dmxAction,"interval");
            _startValue = ActionTools.GetByte(dmxAction,"startValue");
            _value = _startValue;
            Channels = parseChannelProperty.Parse(dmxAction.Channels);
        }
 
        public List<int> Channels { get; set; }
        
        public List<ActionExecuteResult> Execute()
        {
            var result = new List<ActionExecuteResult>();
            _stepCount++;
            if (_stepCount >= _interval)
            {
                if (_value + _step > 255)
                {
                    _value = _startValue;
                }
                else {
                    _value += (byte)_step;
                }

                Console.WriteLine(_value);
                
                _stepCount = 0;
                
                foreach (var channel in Channels)      
                {
                    result.Add( new ActionExecuteResult()
                    {
                        Channel = channel,
                        Value = _value
                    });
                }
            }

            return result;
        }
    }
}