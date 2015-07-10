using System.Drawing.Imaging;
using System.IO;

namespace SpriteSheetPacker.SpriteSheetPack {
    public class ImageWriter {
        public void Write(string path, SpriteSheet spriteSheet) {
            spriteSheet.Image.Save(Path.Combine(path, spriteSheet.Name) + ".png", ImageFormat.Png);
        }
    }
}
