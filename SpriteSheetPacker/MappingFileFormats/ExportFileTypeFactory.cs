using System;

namespace SpriteSheetPacker.MappingFileFormats {
    public class ExportFileTypeFactory {
        public IMappingFile Create(FileType fileType) {
            switch (fileType) {
                case FileType.Json:
                    return new SimpleJSON();
                case FileType.Plist:
                    return new PList();
                case FileType.EngineFormat:
                    return new EngineFormatFile();
                default:
                    throw new NotImplementedException("No mapping for fileType");
            }
        }
    }
}
