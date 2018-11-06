using System.IO;
using SpriteSheetPacker.MappingFileFormats;

namespace SpriteSheetPacker.SpriteSheetPack {
    public class MappingFileWriter {
        public void Write(string filePath, SpriteSheet spriteSheet, IMappingFile mappingFile) {
            mappingFile.Start();
            mappingFile.AddFrames(spriteSheet.FrameList);
            mappingFile.End(spriteSheet.Name + ".png", spriteSheet.Image.Width, spriteSheet.Image.Height);
            File.WriteAllText(Path.Combine(filePath, spriteSheet.Name) + mappingFile.Extension, mappingFile.GetFileContent());
        }
    }
}
