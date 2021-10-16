using System.IO;
using SpriteSheeter.Lib.MappingFileFormats;

namespace SpriteSheeter.Lib.SpriteSheetPack {
    public class MappingFileWriter {
        const string SpriteSheetImageExtension = ".png";

        public void Write(string filePath, SpriteSheet spriteSheet, IMappingFile mappingFile) {
            var imageFileName = spriteSheet.Name + SpriteSheetImageExtension;
            mappingFile.Format(imageFileName, spriteSheet);
            File.WriteAllText(Path.Combine(filePath, spriteSheet.Name) + mappingFile.Extension, mappingFile.GetFileContent());
        }
    }
}
