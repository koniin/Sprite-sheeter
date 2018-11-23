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
            if (argumentCount == 3) {
                fileType = (FileType)Enum.Parse(typeof(FileType), arguments[2]);
            }

            if (argumentCount == 4) {
                var makeBlackWhiteCopies = arguments[3];
                if (!string.IsNullOrWhiteSpace(makeBlackWhiteCopies)) {
                    var dir = new DirectoryInfo(inputpath);
                    var temp_dir = Path.Combine(dir.Parent.FullName, "__temp__images");
                    if (Directory.Exists(temp_dir)) {
                        Directory.Delete(temp_dir, true);
                    }

                    Directory.CreateDirectory(temp_dir);
                    foreach (var f in Directory.EnumerateFiles(inputpath)) {
                        File.Copy(f, Path.Combine(temp_dir, Path.GetFileName(f)));
                    }

                    _spriteSheetPacker.MakeBlackWhiteCopies(temp_dir);

                    if (!Directory.Exists(outputpath)) {
                        Directory.CreateDirectory(outputpath);
                    }

                    string sheet_name = dir.Name;
                    _spriteSheetPacker.PackImagesInFolder(temp_dir, outputpath, _exportFileFactory.Create(fileType), sheet_name);

                    Directory.Delete(temp_dir, true);
                }
            } else {
                _spriteSheetPacker.PackImagesInFolder(inputpath, outputpath, _exportFileFactory.Create(fileType));
            }
            return;
        }
    }
}
