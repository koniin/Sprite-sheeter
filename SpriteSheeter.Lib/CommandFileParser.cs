using SpriteSheeter.Lib.MappingFileFormats;
using SpriteSheeter.Lib.SpriteSheetPack;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SpriteSheeter.Lib {
    public class CommandFileParser {
        private readonly SpriteSheetMaker _spriteSheetMaker;

        public CommandFileParser(SpriteSheetMaker spriteSheetMaker) {
            _spriteSheetMaker = spriteSheetMaker;
        }

        internal string Execute(string[] args) {
            if (args.Length > 1) {
                return $"Wrong number of arguments got {args.Length} expected 1.";
            }

            if(!File.Exists(args[0])) {
                return $"Config file @ {args[0]} does not exist.";
            }

            string[] lines = File.ReadAllLines(args[0]);
            if (lines.Length == 0) {
                return $"Config file @ {args[0]} is empty.";
            }

            return CombineImages(lines);
        }

        private string CombineImages(string[] arguments) {

            StringBuilder failures = new StringBuilder();

            var enumerator = arguments.GetEnumerator();
            while (enumerator.MoveNext()) {
                var cmd = enumerator.Current;

                // combinefolder combinesub  split bw scale
                switch (cmd) {
                    case "combinefolder":
                        string inputpath = null, outputpath = null;
                        if (enumerator.MoveNext()) {
                            inputpath = enumerator.Current as string;
                        }
                        if (enumerator.MoveNext()) {
                            outputpath = enumerator.Current as string;
                        }

                        if(!string.IsNullOrWhiteSpace(inputpath) && !string.IsNullOrWhiteSpace(inputpath)) {
                            _spriteSheetMaker.PackFolder(inputpath, outputpath);
                        }

                        break;
                    case "combinesub":
                        break;
                    case "split":
                        break;
                    case "bw":
                        break;
                    case "scale":
                        break;
                }

            }
            return "All commands executed.";




            //var argumentCount = arguments.Count();
            //if (argumentCount < 2)
            //    throw new ArgumentException($"Too few arguments. Got: {string.Join(", ", arguments)}");

            //var inputpath = arguments[0];
            //var outputpath = arguments[1];
            //var fileType = _userSettings.ExportFileType;
            //if (argumentCount == 3) {
            //    fileType = (FileType)Enum.Parse(typeof(FileType), arguments[2]);
            //}

            //if (argumentCount == 4) {
            //    var makeBlackWhiteCopies = arguments[3];
            //    if (!string.IsNullOrWhiteSpace(makeBlackWhiteCopies)) {
            //        var dir = new DirectoryInfo(inputpath);
            //        var temp_dir = Path.Combine(dir.Parent.FullName, "__temp__images");
            //        if (Directory.Exists(temp_dir)) {
            //            Directory.Delete(temp_dir, true);
            //        }

            //        Directory.CreateDirectory(temp_dir);
            //        foreach (var f in Directory.EnumerateFiles(inputpath)) {
            //            File.Copy(f, Path.Combine(temp_dir, Path.GetFileName(f)));
            //        }

            //        _spriteSheetPacker.MakeBlackWhiteCopies(temp_dir);

            //        if (!Directory.Exists(outputpath)) {
            //            Directory.CreateDirectory(outputpath);
            //        }

            //        string sheet_name = dir.Name;
            //        _spriteSheetPacker.PackImagesInFolder(temp_dir, outputpath, _exportFileFactory.Create(fileType), sheet_name);

            //        Directory.Delete(temp_dir, true);
            //    }
            //} else {
            //    _spriteSheetPacker.PackImagesInFolder(inputpath, outputpath, _exportFileFactory.Create(fileType));
            //}
            //return;
        }
    }
}
