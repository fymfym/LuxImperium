using System.Collections.Generic;

namespace LuxImperium.Models
{
    public class DmxScene
    {
        public string Name { get; set; }
        public List<DmxFixture> Fixtures { get; set; }
        public List<DmxAction> Actions { get; set; }
        public DmxAction StartAction { get; set; }
        public DmxAction StopAction { get; set; }
    }
}