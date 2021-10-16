using SpriteSheeter.Lib.SpriteSheetPack;

namespace SpriteSheeter.Lib.ImageManipulation {
    public interface IFrameListCombiner {
        SpriteSheet Combine(FrameList frameList);
    }
}