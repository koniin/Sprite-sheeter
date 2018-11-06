using System;

namespace SpriteSheetPacker.MappingFileFormats {
    public class ExportFileTypeFactory {
        public IMappingFile Create(FileType fileType) {
            switch (fileType) {
                case FileType.Json:
                    throw new NotImplementedException("Json filetype is not implemented");
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
