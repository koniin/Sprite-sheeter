using System;
using SpriteSheeter.Lib;

namespace SpriteSheeter.Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var userSettings = UserSettings.CreateFromEnvironmentVariable();
            Console.WriteLine($"ExportFileType: {userSettings.ExportFileType}");
            var spriteSheetMaker = new SpriteSheetMaker(userSettings);
            var ss = new SpriteSheeterCommands(spriteSheetMaker);

            if(args.Length > 0) {
                Console.WriteLine(ss.eval(string.Join(' ', args)));
                return;
            }

            ss.Interactive(":> ");
        }
    }
}
