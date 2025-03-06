using System.Collections.Generic;
using LuxImperium.Models;

namespace LuxImperium.Services.Actions
{
    public class ActionOnOff : IAction
    {

        private readonly int _onStep;
        private readonly int _offStep;
        private readonly byte _onValue;
        private readonly byte _offValue;
        private byte _value;

        private enum EState
        {
            FT_WAITING_FOR_ON,
            FT_WAITING_FOR_OFF
        }

        private EState _state = EState.FT_WAITING_FOR_ON; 
        private int _stepCount;
        
        public ActionOnOff(
            IParseChannelProperty parseChannelProperty,
            DmxAction dmxAction)
        {
            _onStep = ActionTools.GetInt(dmxAction,"onStep");
            _offStep = ActionTools.GetInt(dmxAction,"offStep");
            _onValue = ActionTools.GetByte(dmxAction,"onValue");
            _offValue = ActionTools.GetByte(dmxAction,"offValue");
            Channels = parseChannelProperty.Parse(dmxAction.Channels);
            
        }

        public List<int> Channels { get; set; }
      
        public List<ActionExecuteResult> Execute()
        {
            var result = new List<ActionExecuteResult>();
            _stepCount++;
            if (_stepCount >= _onStep && _state == EState.FT_WAITING_FOR_ON)
            {
                _value = _onValue;
                _state = EState.FT_WAITING_FOR_OFF;
                _stepCount = 0;

            }

            if (_stepCount >= _offStep && _state == EState.FT_WAITING_FOR_OFF)
            {
                _value = _offValue;
                _state = EState.FT_WAITING_FOR_ON;
                _stepCount = 0;
            }
                
            
            foreach (var channel in Channels)      
            {
                result.Add( new ActionExecuteResult()
                {
                    Channel = channel,
                    Value = _value
                });
            }

            return result;
        }
    }
}