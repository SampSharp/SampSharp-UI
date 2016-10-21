using SampSharp.GameMode;

namespace SampSharp.UI
{
    public class ContainerControl : Control
    {
        public ContainerControl()
        {
            Controls = new ControlCollection(this);
        }
        
        public ControlCollection Controls { get; }
    }
}