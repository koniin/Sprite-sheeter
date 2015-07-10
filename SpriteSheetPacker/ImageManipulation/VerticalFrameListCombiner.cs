using System;
using System.Drawing;
using SpriteSheetPacker.SpriteSheetPack;

namespace SpriteSheetPacker.ImageManipulation {
    public class VerticalFrameListCombiner : IFrameListCombiner {
        public SpriteSheet Combine(FrameList frameList) {
            Bitmap finalImage = null;
            try {
                var width = 0;
                var height = 0;
                
                foreach (var frame in frameList.Frames) {
                    //update the size of the final bitmap
                    width += frame.Width;
                    height = frame.Height > height ? frame.Height : height;
                }

                finalImage = new Bitmap(width, height);

                using (Graphics g = Graphics.FromImage(finalImage)) {
                    //set background color
                    g.Clear(Color.Transparent);

                    //go through each image and draw it on the final image
                    int offsetX = 0, offsetY = 0;
                    foreach (var frame in frameList.Frames) {
                        frame.PositionInSheetX = offsetX;
                        frame.PositionInSheetY = offsetY;
                        g.DrawImage(frame.Bitmap, new Rectangle(offsetX, offsetY, frame.Width, frame.Height));
                        offsetX += frame.Width;
                    }
                }
            } catch (Exception) {
                if (finalImage != null)
                    finalImage.Dispose();
                throw;
            } finally {
                ////clean up memory
                //foreach (var image in frameList.Frames) {
                //    image.Bitmap.Dispose();
                //}
            }

            return new SpriteSheet(frameList, finalImage, frameList.Name);
        }
    }
}
