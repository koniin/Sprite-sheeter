using System.Drawing;
using SpriteSheetPacker.SpriteSheetPack;

namespace SpriteSheetPacker.ImageManipulation {
    public class SquareFrameListCombiner : IFrameListCombiner {
        public SpriteSheet Combine(FrameList frameList){
            Bitmap finalImage = null;

            // http://codeincomplete.com/posts/2011/5/7/bin_packing/
            // http://jwezorek.com/2013/01/sprite-packing-in-python/
            // http://www.blackpawn.com/texts/lightmaps/default.html

            return new SpriteSheet(frameList, finalImage, frameList.Name);
        }
    }
}
