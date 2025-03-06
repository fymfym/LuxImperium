using System.Collections.Generic;

namespace LuxImperium.Services
{
    public interface IParseChannelProperty
    {
        List<int> Parse(string channelText);
    }
}