using System;
using SpriteSheeter.Lib;

namespace SpriteSheeter.Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var userSettings = DefaultUserSettingsReader.Read();
            var spriteSheetMaker = new SpriteSheetMaker(new UserSettings(Lib.MappingFileFormats.FileType.SimpleData));
            SpriteSheeterCommands ss = new SpriteSheeterCommands(spriteSheetMaker);

            if(args.Length > 0) {
                ss.Execute(args);
                return;
            }

            ss.Interactive(":> ");
        }
    }
}
