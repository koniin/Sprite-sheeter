using System;
using System.IO;
using System.Linq;
using SpriteSheetPacker.ImageManipulation;
using SpriteSheetPacker.MappingFileFormats;

namespace SpriteSheetPacker {
    public class CommandFileParser {
        private SpriteSheetPack.SpriteSheetPacker _spriteSheetPacker;
        private UserSettings _userSettings;
        private ExportFileTypeFactory _exportFileFactory;

        public CommandFileParser() {
            _spriteSheetPacker = new SpriteSheetPack.SpriteSheetPacker(new SquareFrameListCombiner());
            _userSettings = new UserSettings();
            _exportFileFactory = new ExportFileTypeFactory();
        }

        internal void Execute(string[] args) {
            int argumentCount = args.Count();
            if (argumentCount == 1) {
                string[] lines = File.ReadAllLines(args[0]);
                var lineCount = lines.Count();
                if (lineCount >= 2) {
                    var inputpath = lines[0];
                    var outputpath = lines[1];

                    var fileType = _userSettings.ExportFileType;
                    if (lineCount >= 3) {
                        fileType = (FileType)Enum.Parse(typeof(FileType), lines[2]);
                    }

                    _spriteSheetPacker.PackImagesInFolder(inputpath, outputpath, _exportFileFactory.Create(fileType));
                    return;
                }
                throw new ArgumentException($"Malformed config file {string.Join(", ", args)} => {string.Join(", ", lines)}");
            }
            throw new ArgumentException($"Unknown argument/s {string.Join(", ", args)}");
        }
    }
}
