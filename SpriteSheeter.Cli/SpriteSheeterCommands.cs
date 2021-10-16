using SpriteSheeter.Lib;
using System;

namespace SpriteSheeter.Cli {
    internal class SpriteSheeterCommands : Commands.CommandInterface {
        SpriteSheetMaker _spriteSheeter;

        public SpriteSheeterCommands(SpriteSheetMaker spriteSheeter) {
            _spriteSheeter = spriteSheeter;
        }

        private string CombineAllInFolder(string[] args) {
            // Console.Write("\n Enter input path: ");
            // var inputpath = Console.ReadLine();
            // Console.Write("\n Enter output path: ");
            // var outputpath = Console.ReadLine();
            _spriteSheeter.PackFolder(args[0], args[1]);
            return "Created new sheet in " + args[1];
        }

        // private string TwoArg(string[] args)
        // {
        //     Console.WriteLine($"Got args: [{args[0]}, {args[1]}]");
        //     return "TwoArg success";
        // }

        public override void register_commands() {
            register_command(CombineAllInFolder, 2, "combinefolder", "Combines all sprites from input folder into a spritesheet in output folder");
            // register_command(TwoArg, 2, "two", "Two args required.");
        }


        public void Execute(string[] args) {
            throw new NotImplementedException();
            // var commandFileParser = new CommandFileParser();
            // commandFileParser.Execute(args);
        }
    }
}