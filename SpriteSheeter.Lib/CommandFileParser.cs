using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpriteSheeter.Lib {
    public class CommandFileParser {
        private readonly string[] AVAILABLE_COMMANDS = new string[6] {
            "combinefolder", "combinesub", "split", "bw", "scale", "filetype"
        };

        public (string, string[]) Parse(string[] args) {
            if (args.Length > 1) {
                return ($"Wrong number of arguments got {args.Length} expected 1.", Array.Empty<string>());
            }

            if(!File.Exists(args[0])) {
                return ($"Config file @ {args[0]} does not exist.", Array.Empty<string>());
            }

            string[] lines = File.ReadAllLines(args[0]);
            if (lines.Length == 0) {
                return ($"Config file @ {args[0]} is empty.", Array.Empty<string>());
            }

            return ParseCommands(lines);
        }

        private (string, string[]) ParseCommands(string[] arguments) {
            int rowCounter = 0;
            var enumerator = arguments.GetEnumerator();
            List<string> commandResult = new List<string>();
            string currentCommand = "";
            while (enumerator.MoveNext()) {
                var row = enumerator.Current as string;
                
                if(AVAILABLE_COMMANDS.Contains(row.ToLowerInvariant())) {
                    rowCounter++;
                    if(!string.IsNullOrWhiteSpace(currentCommand)) {
                        commandResult.Add(currentCommand);
                    }
                    currentCommand = row;
                } else {
                    currentCommand += " " + row;
                }
            }
            commandResult.Add(currentCommand);
            return ("Succes parsing file.", commandResult.ToArray());
        }

        //private static void Ok() {
        //    switch (row) {
        //        case "combinefolder": {
        //                string inputpath = null, outputpath = null;
        //                FileType fileType = _spriteSheetMaker.ExportFiletype;
        //                if (enumerator.MoveNext()) {
        //                    inputpath = enumerator.Current as string;
        //                }
        //                if (enumerator.MoveNext()) {
        //                    outputpath = enumerator.Current as string;
        //                }
        //                if (enumerator.MoveNext()) {
        //                    var ft = enumerator.Current as string;
        //                    fileType = Enum.Parse<FileType>(ft, true);
        //                }

        //                if (IsValidDirectoryArgument(inputpath) && IsValidDirectoryArgument(outputpath)) {
        //                    _spriteSheetMaker.PackFolder(inputpath, outputpath, fileType);
        //                } else {
        //                    return $"Invalid argument to {row} at row: {rowCounter} [inputpath: {inputpath}, outputpath: {outputpath}]";
        //                }
        //            }
        //            break;
        //        case "combinesub": {
        //                string inputpath = null;
        //                FileType fileType = _spriteSheetMaker.ExportFiletype;
        //                if (enumerator.MoveNext()) {
        //                    inputpath = enumerator.Current as string;
        //                }
        //                if (enumerator.MoveNext()) {
        //                    var ft = enumerator.Current as string;
        //                    fileType = Enum.Parse<FileType>(ft, true);
        //                }

        //                if (IsValidDirectoryArgument(inputpath)) {
        //                    _spriteSheetMaker.CombineFromSubFolders(inputpath, fileType);
        //                } else {
        //                    return $"Invalid argument to {row} at row: {rowCounter} [inputpath: {inputpath}]";
        //                }
        //            }
        //            break;
        //        case "split": {
        //                string requestedSize = null, inputpath = null;
        //                if (enumerator.MoveNext()) {
        //                    requestedSize = enumerator.Current as string;
        //                }
        //                if (enumerator.MoveNext()) {
        //                    inputpath = enumerator.Current as string;
        //                }

        //                if (IsValidSizeArgument(requestedSize) && IsValidDirectoryArgument(inputpath)) {
        //                    _spriteSheetMaker.SplitSheet(requestedSize, inputpath);
        //                } else {
        //                    return $"Invalid argument to {row} at row: {rowCounter} [requestedSize: {requestedSize},inputpath: {inputpath}]";
        //                }
        //            }
        //            break;
        //        case "bw": {
        //                string inputpath = null;
        //                if (enumerator.MoveNext()) {
        //                    inputpath = enumerator.Current as string;
        //                }

        //                if (IsValidDirectoryArgument(inputpath)) {
        //                    _spriteSheetMaker.MakeBlackAndWhiteCopies(inputpath);
        //                } else {
        //                    return $"Invalid argument to {row} at row: {rowCounter} [inputpath: {inputpath}]";
        //                }
        //            }
        //            break;
        //        case "scale": {
        //                string inputpath = null, outputpath = null;
        //                if (enumerator.MoveNext()) {
        //                    inputpath = enumerator.Current as string;
        //                }
        //                if (enumerator.MoveNext()) {
        //                    outputpath = enumerator.Current as string;
        //                }

        //                if (IsValidDirectoryArgument(inputpath) && IsValidDirectoryArgument(outputpath)) {
        //                    _spriteSheetMaker.ScaleImages(inputpath, outputpath);
        //                } else {
        //                    return $"Invalid argument to {row} at row: {rowCounter} [inputpath: {inputpath}, outputpath: {outputpath}]";
        //                }
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //private static bool IsValidDirectoryArgument(string inputpath) {
        //    return !string.IsNullOrWhiteSpace(inputpath) && Directory.Exists(inputpath);
        //}

        //private static bool IsValidSizeArgument(string requestedSize) {
        //    return !string.IsNullOrWhiteSpace(requestedSize) && int.TryParse(requestedSize, out _);
        //}
    }
}
