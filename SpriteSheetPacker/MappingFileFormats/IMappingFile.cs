using SpriteSheetPacker.SpriteSheetPack;

namespace SpriteSheetPacker.MappingFileFormats {
    public interface IMappingFile {
        string Extension { get; }
        void Format(string imageFileName, SpriteSheet spriteSheet);
        string GetFileContent();
    }
}