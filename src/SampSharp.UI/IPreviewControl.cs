using SampSharp.GameMode;

namespace SampSharp.UI
{
    public interface IPreviewControl
    {
        int PreviewModel { get; set; }
        int PreviewPrimaryColor { get; set; }
        Vector3 PreviewRotation { get; set; }
        int PreviewSecondaryColor { get; set; }
        float PreviewZoom { get; set; }
    }
}