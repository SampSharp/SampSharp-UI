using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;

namespace SampSharp.UI
{
    public class Label : TextDrawControl
    {
        public Label()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Font = TextDrawFont.Normal;
            Proportional = true;
            LetterSize = new Vector2(0.18f, 0.9f);
            Shadow = 1;
            ForeColor = 0xFFFFFFFF;
            BackColor = 0x00000033;
        }
    }
}