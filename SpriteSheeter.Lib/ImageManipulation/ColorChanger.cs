using System.Drawing;

namespace SpriteSheeter.Lib.ImageManipulation {
    public class ColorChanger {
        public static void ChangeAllColorsEqualTo(string inputFileName, string outputFilename, Color fromColor, Color outColor) {
            using (Bitmap srcImage = (Bitmap)Image.FromFile(inputFileName)) {
                for (int i = 0; i < srcImage.Width; i++) {
                    for (int j = 0; j < srcImage.Height; j++) {
                        var c = srcImage.GetPixel(i, j);
                        if (c.Equals(fromColor)) {
                            srcImage.SetPixel(i, j, outColor);
                        }
                    }
                }
                srcImage.Save(outputFilename);
            }
        }

        public static void ChangeAllColorsNotEqualTo(string inputFileName, string outputFilename, Color fromColor, Color outColor) {
            using (Bitmap srcImage = (Bitmap)Image.FromFile(inputFileName)) {
                for (int i = 0; i < srcImage.Width; i++) {
                    for (int j = 0; j < srcImage.Height; j++) {
                        var c = srcImage.GetPixel(i, j);
                        if (!c.Equals(fromColor)) {
                            srcImage.SetPixel(i, j, outColor);
                        } 
                    }
                }
                srcImage.Save(outputFilename);
            }
        }
    }
}
