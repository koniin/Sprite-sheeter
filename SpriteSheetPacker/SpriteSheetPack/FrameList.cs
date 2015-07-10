using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace SpriteSheetPacker.SpriteSheetPack {
    public class FrameList {
        public string Name { get; set; }
        public List<Frame> Frames { get; set; }

        public FrameList() {
            Frames = new List<Frame>();
        }

        public void AddFrame(string file) {
            var fileName = Path.GetFileName(file);
            var bmp = new Bitmap(file);
            var frame = new Frame(fileName, bmp);
            Frames.Add(frame);
        }
    }
}
