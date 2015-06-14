using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpriteSheetPacker {
    class ImageCombinator {
        private delegate void UpdateImageDimensions(Bitmap image, ref int width, ref int height);
        private delegate int DrawOffset(Graphics g, Bitmap image, int offset);

        public static Bitmap CombineVertical(Bitmap[] files){
            return CombineImages(files, UpdateVerticalImageDimensions, DrawVertical);
        }

        public static Bitmap CombineHorizontal(Bitmap[] files) {
            return CombineImages(files, UpdateHorizontalDimensions, DrawHorizontal);
        }

        private static Bitmap CombineImages(Bitmap[] files, UpdateImageDimensions funcUpdateImageDimensions, DrawOffset drawOffset) {
            Bitmap finalImage = null;
            try{
                var width = 0;
                var height = 0;

                foreach (var image in files){
                    //update the size of the final bitmap
                    funcUpdateImageDimensions(image, ref width, ref height);
                }
                finalImage = new Bitmap(width, height);

                using (Graphics g = Graphics.FromImage(finalImage)){
                    //set background color
                    g.Clear(Color.Transparent);

                    //go through each image and draw it on the final image
                    int offset = 0;
                    foreach (Bitmap image in files){
                        offset = drawOffset(g, image, offset);
                    }
                }
            }
            catch (Exception){
                if (finalImage != null)
                    finalImage.Dispose();
                throw;
            }
            finally{
                //clean up memory
                foreach (var image in files){
                    image.Dispose();
                }
            }
            return finalImage;
        }

        public static Bitmap CombineHorizontal(string[] files) {
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
                    width += bitmap.Width;
                    height = bitmap.Height > height ? bitmap.Height : height;

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
                        g.DrawImage(image, new Rectangle(offset, 0, image.Width, image.Height));
                        offset += image.Width;
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

        private static void UpdateHorizontalDimensions(Bitmap image, ref int width, ref int height) {
            width += image.Width;
            height = image.Height > height ? image.Height : height;
        }

        private static void UpdateVerticalImageDimensions(Bitmap image, ref int width, ref int height) {
            width = image.Width > width ? image.Width : width;
            height += image.Height;
        }

        private static int DrawVertical(Graphics g, Bitmap image, int offset) {
            g.DrawImage(image, new Rectangle(0, offset, image.Width, image.Height));
            offset += image.Height;
            return offset;
        }

        private static int DrawHorizontal(Graphics g, Bitmap image, int offset) {
            g.DrawImage(image, new Rectangle(offset, 0, image.Width, image.Height));
            offset += image.Width;
            return offset;
        }
    }
}
