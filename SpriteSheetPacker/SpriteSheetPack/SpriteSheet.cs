using System.Drawing;

namespace SpriteSheetPacker.SpriteSheetPack {
    public class SpriteSheet {
        public SpriteSheet(FrameList frameList, Bitmap image, string name) {
            FrameList = frameList;
            Image = image;
            Name = name;
        }

        public FrameList FrameList { get; set; }
        public Bitmap Image { get; set; }
        public string Name { get; set; }
    }
}
