using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpriteSheetPacker {
    class ImageCombinator {
        public static Bitmap Combine(string[] files) {
            //read all images into memory
            var images = new List<Bitmap>();
            Bitmap finalImage = null;

            try {
                var width = 0;
                var height = 0;

                foreach (var image in files) {
                    //create a Bitmap from the file and add it to the list
                    var bitmap = new Bitmap(image);

                    //update the size of the final bitmap
                    width = bitmap.Width > width ? bitmap.Width : width;
                    height += bitmap.Height;

                    images.Add(bitmap);
                }

                //create a bitmap to hold the combined image
                finalImage = new Bitmap(width, height);

                //get a graphics object from the image so we can draw on it
                using (var g = Graphics.FromImage(finalImage)) {
                    //set background color
                    g.Clear(Color.Transparent);

                    //go through each image and draw it on the final image
                    var offset = 0;
                    foreach (var image in images) {
                        g.DrawImage(image,
                          new Rectangle(0, offset, image.Width, image.Height));
                        offset += image.Height;
                    }
                }

                return finalImage;
            } catch (Exception ex) {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            } finally {
                //clean up memory
                foreach (var image in images) {
                    image.Dispose();
                }
            }
        }
    }
}
