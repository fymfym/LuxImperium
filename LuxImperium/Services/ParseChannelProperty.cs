using System;
using System.Collections.Generic;
using System.Linq;

namespace LuxImperium.Services
{
    public class ParseChannelProperty : IParseChannelProperty
    {
        public List<int> Parse(string channelText)
        {
            var result = new List<int>();

            if (string.IsNullOrWhiteSpace(channelText))
            {
                return new List<int>();
            }
            
            if (channelText.Equals("all", StringComparison.InvariantCultureIgnoreCase))
            {
                for (var i = 1; i <= 512; i++)
                {
                    result.Add(i);    
                }

                return result;
            }

            var lst = channelText.Split(',');
            result.AddRange(lst.Select(int.Parse));
            return result;
        }
    }
}