using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;

namespace SampSharp.UI
{
    public interface ITextControl
    {
        TextDrawFont Font { get; set; }
        Color ForeColor { get; set; }
        Vector2 LetterSize { get; set; }
        int Outline { get; set; }
        bool Proportional { get; set; }
        int Shadow { get; set; }
        string Text { get; set; }
    }
}