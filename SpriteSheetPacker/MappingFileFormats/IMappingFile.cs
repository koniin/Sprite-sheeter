using SpriteSheetPacker.SpriteSheetPack;

namespace SpriteSheetPacker.MappingFileFormats {
    public interface IMappingFile {
        void Start();
        void AddFrame(string fileName, int x, int y, int width, int height);
        void End(string fileName, int width, int height);
        void AddFrames(FrameList sheet);
        string Extension { get; }

        string GetFileContent();
    }
}