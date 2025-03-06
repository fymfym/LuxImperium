using System;
using LuxImperium.Models;

namespace LuxImperium.Services
{
    public static class ActionTools
    {
        public static string GetString(DmxAction dmxAction, string name)
        {
            dmxAction.ActionValues.TryGetValue(name, out var value);
            return value;
        }
        
        public static byte GetByte(DmxAction dmxAction, string name)
        {
            if (dmxAction == null)
            {
                throw new NullReferenceException("dmxAction must not be null");
            }
            if (dmxAction.ActionValues == null)
            {
                return 0;
            }
            
            var v = dmxAction.ActionValues.TryGetValue(name, out var value);
            return v ? byte.Parse(value) : (byte)0;
        }
        
        public static bool GetBool(DmxAction dmxAction, string name)
        {
            var v = dmxAction.ActionValues.TryGetValue(name, out var value);
            return v && bool.Parse(value);
        }

        public static  int GetInt(DmxAction dmxAction, string name)
        {
            var v = dmxAction.ActionValues.TryGetValue(name, out var value);
            return v ? int.Parse(value) : 0;
        }
        
    }
}