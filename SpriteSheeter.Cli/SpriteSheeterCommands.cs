using SpriteSheeter.Lib;
using SpriteSheeter.Lib.MappingFileFormats;
using System;

namespace SpriteSheeter.Cli {
    internal class SpriteSheeterCommands : Commands.CommandInterface {
        SpriteSheetMaker _spriteSheeter;

        public SpriteSheeterCommands(SpriteSheetMaker spriteSheeter) {
            _spriteSheeter = spriteSheeter;
        }

        private string ExecuteConfigFile(string[] args) {
            var (status, commands) = _spriteSheeter.ParseConfigFile(args);

            // We need to create a new command interface because otherwise 
            // we mess up state if we eval inside an eval
            var spc = new SpriteSheeterCommands(_spriteSheeter);
            foreach (var command in commands) {
                Console.WriteLine(spc.eval(command));
            }

            return status;
        }

        private string CombineAllInFolder(string[] args) {
            return _spriteSheeter.PackFolder(args[0], args[1]);
        }

        private string CombineFromSubFolders(string[] args) {
            return _spriteSheeter.CombineFromSubFolders(args[0]);
        }

        private string SplitSheet(string[] args) {
            return _spriteSheeter.SplitSheet(args[0], args[1]);
        }

        private string MakeBlackAndWhiteCopies(string[] args) {
            return _spriteSheeter.MakeBlackAndWhiteCopies(args[0]);
        }

        private string ScaleImages(string[] args) {
            return _spriteSheeter.ScaleImages(args[0], args[1]);
        }

        private string SetDefaultExportType(string[] args) {
            if (Enum.TryParse(args[0], out FileType fileType)) {
                return _spriteSheeter.SetDefaultExportType(fileType);
            } else {
                return $"Could not parse: {args[0]} into a fileType";
            }
        }
        
        public override void register_commands() {
            register_command(ExecuteConfigFile, 1, "cfg", "Execute commands from a config file [filepath]");
            register_command(CombineAllInFolder, 2, "combinefolder", "Combines all sprites from input folder into a spritesheet in output folder [inputpath, outputpath]");
            register_command(CombineFromSubFolders, 1, "combinesub", "Combines all sprites from subfolders in folder into a spritesheet in folder [inputpath]");
            register_command(SplitSheet, 2, "split", "Split a sheet into frames of requestedSize x requestedSize [size, inputpath]");
            register_command(MakeBlackAndWhiteCopies, 1, "bw", "Creates black and white copes of all images in inputpath. [inputpath]");
            register_command(ScaleImages, 2, "scale", "Resizes images to the requested size in the inputpath. [size, inputpath]");
            register_command(SetDefaultExportType, 1, "filetype", "Set the default export type (sets in environment variable). [filetype]");
        }
    }
}