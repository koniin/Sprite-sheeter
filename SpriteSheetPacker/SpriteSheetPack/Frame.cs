using System.Drawing;
using SpriteSheetPacker.ImageManipulation;

namespace SpriteSheetPacker.SpriteSheetPack {
    public class Frame {
        public Bitmap Bitmap { get; private set; }
        public string FileName { get; private set; } 
        public int Width { get { return Bitmap.Width; } }
        public int Height { get { return Bitmap.Height; } }
        public int PositionInSheetX { get; set; }
        public int PositionInSheetY { get; set; }
        public Node Fit { get; set; }

        public Frame(string fileName, Bitmap bitmap) {
            FileName = fileName;
            Bitmap = bitmap;
        }
    }
}
