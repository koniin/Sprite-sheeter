using SpriteSheeter.Lib.ImageManipulation;
using SpriteSheeter.Lib.MappingFileFormats;
using SpriteSheeter.Lib.SpriteSheetPack;

namespace SpriteSheeter.Lib
{
    public class SpriteSheetMaker
    {
        private SpriteSheetPacker _spriteSheetPacker;
        private UserSettings _userSettings;
        private ExportFileTypeFactory _exportFileFactory;
        
        public SpriteSheetMaker(UserSettings userSettings)
        {
            _spriteSheetPacker = new SpriteSheetPacker(new SquareFrameListCombiner());
            _userSettings = userSettings;
            _exportFileFactory = new ExportFileTypeFactory();
        }


        public void ExecuteCommandFile(string[] args) {
            var commandFileParser = new CommandFileParser(
                _spriteSheetPacker,
                _userSettings,
                _exportFileFactory);

            commandFileParser.Execute(args);
        }

        public void PackFolder(string inputpath, string outputpath)
        {
            _spriteSheetPacker.PackImagesInFolder(inputpath, outputpath, _exportFileFactory.Create(_userSettings.ExportFileType));
        }

        /*
         
        private static void CombineFromSubFolders(){
            Console.Write("\n Enter path: ");
            var path = Console.ReadLine();
            _spriteSheetPacker.PackImagesFromSubfolders(path, _exportFileFactory.Create(_userSettings.ExportFileType));
            _status = "Created new sheet @ " + path;
        }

        private static void SplitSheet() {
            short size;
            string requestedSize = string.Empty, inputpath = string.Empty;
            while (!short.TryParse(requestedSize, out size)) {
                Console.Write("\n Enter size (number greater than 0): ");
                requestedSize = Console.ReadLine();
            }
            while (!System.IO.File.Exists(inputpath)) {
                Console.Write("\n Enter input image path: ");
                inputpath = Console.ReadLine();
            }
            var newPath = _spriteSheetPacker.SplitImage(inputpath, size);
            _status = "Created new folder with images in " + newPath;
        }

        private static void MakeBlackAndWhiteCopies() {
            Console.Write("\n Enter input path: ");
            var inputpath = Console.ReadLine();
            _spriteSheetPacker.MakeBlackWhiteCopies(inputpath);
            _status = "Made black and white copies in: " + inputpath;
        }

        private static void ScaleImages() {
            string requestedSize = string.Empty, inputpath = string.Empty;
            int newSize = 0;
            while (!int.TryParse(requestedSize, out int size)) {
                Console.Write("\n Enter new size (number greater than 0): ");
                requestedSize = Console.ReadLine();
                newSize = int.Parse(requestedSize);
            }
            while (!System.IO.Directory.Exists(inputpath)) {
                Console.Write("\n Enter input images path: ");
                inputpath = Console.ReadLine();
            }

            var newPath =_spriteSheetPacker.ResizeImages(inputpath, newSize);
            _status = $"Created new images in: {newPath}";
        }

        private static void SetDefaultExportType() {
            Console.Write("\n Choose file type:");
            var choices = Enum.GetNames(typeof (FileType));
            int i = 0;
            foreach (var name in choices) {
                i++;
                Console.Write("\n " + i + ". " + name);
            }

            short choice = 0;
            string requestedChoice = string.Empty;
            while (!short.TryParse(requestedChoice, out choice)) {
                Console.Write("\n : ");
                requestedChoice = Console.ReadLine();
            }

            _userSettings.ExportFileType = (FileType)Enum.GetValues(typeof(FileType)).GetValue(choice - 1);
            _userSettings.Save();
            _status = "Export file type set to: " + _userSettings.ExportFileType;
        }
         */
    }
}