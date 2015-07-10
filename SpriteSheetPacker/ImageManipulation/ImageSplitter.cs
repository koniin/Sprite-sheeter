using System.Collections.Generic;
using System.Drawing;

namespace SpriteSheetPacker.ImageManipulation {
    public class ImageSplitter {
        public IEnumerable<Bitmap> Split(string file, int spriteSize){
            var bitmap = new Bitmap(file);

            var sprites = new List<Bitmap>();
            for (int y = 0;y < bitmap.Height;y += spriteSize) {
                for (int x = 0; x < bitmap.Width; x += spriteSize){
                    var sprite = new Bitmap(spriteSize, spriteSize);
                    using (Graphics g = Graphics.FromImage(sprite)) {
                        g.Clear(Color.Transparent);
                        g.DrawImage(bitmap, new Rectangle(0, 0, spriteSize, spriteSize), new Rectangle(x, y, spriteSize, spriteSize), GraphicsUnit.Pixel);
                    }
                    sprites.Add(sprite);
                }
            }
            return sprites;
        }
    }
}
