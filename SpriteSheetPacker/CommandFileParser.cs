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
                CombineImages(lines);
            } else if (argumentCount >= 2) {
                CombineImages(args);
            }
        }

        private void CombineImages(string[] arguments) {
            var argumentCount = arguments.Count();
            if (argumentCount < 2)
                throw new ArgumentException($"Too few arguments. Got: {string.Join(", ", arguments)}");

            var inputpath = arguments[0];
            var outputpath = arguments[1];
            var fileType = _userSettings.ExportFileType;
            if (argumentCount >= 3) {
                fileType = (FileType)Enum.Parse(typeof(FileType), arguments[2]);
            }

            _spriteSheetPacker.PackImagesInFolder(inputpath, outputpath, _exportFileFactory.Create(fileType));
            return;
        }
    }
}
