using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace SpriteSheetPacker {
    class ImageCombinator {
        public static ISpriteSheetExport Exporter;

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
                Exporter.Start();
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
                    foreach (Bitmap image in files) {
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
            return CombineHorizontal(files.Select(f => new Bitmap(f) { Tag = Path.GetFileName(f) }).ToArray());
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
            Exporter.AddFrame((string)image.Tag, 0, offset, image.Width, image.Height);
            return offset;
        }

        private static int DrawHorizontal(Graphics g, Bitmap image, int offset) {
            g.DrawImage(image, new Rectangle(offset, 0, image.Width, image.Height));
            offset += image.Width;
            Exporter.AddFrame((string)image.Tag, offset, 0, image.Width, image.Height);
            return offset;
        }
    }
}
