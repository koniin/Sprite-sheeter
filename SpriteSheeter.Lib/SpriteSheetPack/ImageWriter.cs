using System.Drawing.Imaging;
using System.IO;

namespace SpriteSheeter.Lib.SpriteSheetPack {
    public class ImageWriter {
        public void Write(string path, SpriteSheet spriteSheet) {
            if (!Directory.Exists(path)) {
                throw new System.ArgumentException($"Directory does not exist: {path}");
            }

            var p = Path.Combine(path, spriteSheet.Name);
            spriteSheet.Image.Save($"{p}.png", ImageFormat.Png);
        }
    }
}
