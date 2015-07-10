using SpriteSheetPacker.SpriteSheetPack;

namespace SpriteSheetPacker.ImageManipulation {
    public interface IFrameListCombiner {
        SpriteSheet Combine(FrameList frameList);
    }
}