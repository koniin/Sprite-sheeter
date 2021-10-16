using System;

namespace SpriteSheeter.Lib.MappingFileFormats {
    public class ExportFileTypeFactory {
        public IMappingFile Create(FileType fileType) {
            switch (fileType) {
                case FileType.Json:
                    return new SimpleJSON();
                case FileType.Plist:
                    return new PList();
                case FileType.SimpleData:
                    return new SimpleData();
                default:
                    throw new NotImplementedException("No mapping for fileType");
            }
        }
    }
}
