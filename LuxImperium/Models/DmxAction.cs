using System.Collections.Generic;

namespace LuxImperium.Models
{
    public class DmxAction
    {
        public string Fixture { get; set; }
        public string Action { get; set; }
        public bool Disabled { get; set; }
        public string Channels { get; set; }
        public Dictionary<string,string> ActionValues { get; set; }
    }
}