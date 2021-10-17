using SpriteSheeter.Lib.MappingFileFormats;
using System;

namespace SpriteSheeter.Lib {
    public class UserSettings {
        public const string ENVIRONMENTVARIABLE_NAME = "SPRITESHEETER_FILETYPE";
        public FileType ExportFileType { get; set; }

        public UserSettings(FileType exportFileType) {
            ExportFileType = exportFileType;
        }

        public UserSettings(string fileType) {
            if(Enum.TryParse(fileType, out FileType ft)) {
                ExportFileType = ft;
            } else {
                ExportFileType = FileType.SimpleData;
            }
        }

        public static UserSettings CreateFromEnvironmentVariable() {
            return new UserSettings(Environment.GetEnvironmentVariable(ENVIRONMENTVARIABLE_NAME));
        }
    }
}
