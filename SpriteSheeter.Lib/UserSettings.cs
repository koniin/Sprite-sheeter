using SpriteSheeter.Lib.MappingFileFormats;

namespace SpriteSheeter.Lib {
    public class UserSettings {
        public FileType ExportFileType { get; set; }

        public UserSettings(FileType exportFileType) {
            ExportFileType = exportFileType;
        }
    }
}
