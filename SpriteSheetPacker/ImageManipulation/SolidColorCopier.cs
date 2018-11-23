using System.Drawing;

namespace SpriteSheetPacker.ImageManipulation {
    public class SolidColorCopier {
        public static void ChangeAllColorsTo(string input, string output, Color outColor) {
            using (Bitmap srcImage = (Bitmap)Image.FromFile(input)) {
                Color transparent = Color.FromArgb(0, 0, 0, 0);
                for (int i = 0; i < srcImage.Width; i++) {
                    for (int j = 0; j < srcImage.Height; j++) {
                        var c = srcImage.GetPixel(i, j);
                        if (!c.Equals(transparent)) {
                            srcImage.SetPixel(i, j, outColor);
                        } 
                    }
                }
                srcImage.Save(output);
            }
        }
    }
}
