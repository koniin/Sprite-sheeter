using SpriteSheeter.Lib.SpriteSheetPack;

namespace SpriteSheeter.Lib.MappingFileFormats {
    public interface IMappingFile {
        string Extension { get; }
        void Format(string imageFileName, SpriteSheet spriteSheet);
        string GetFileContent();
    }
}