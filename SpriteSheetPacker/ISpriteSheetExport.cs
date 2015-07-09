namespace SpriteSheetPacker {
    public interface ISpriteSheetExport {
        void Start();
        void AddFrame(string fileName, int x, int y, int wdith, int height);
        void End(string fileName, int width, int height);
    }
}