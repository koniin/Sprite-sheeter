using System;
using System.Security.Cryptography;
using SpriteSheetPacker.ImageManipulation;
using SpriteSheetPacker.MappingFileFormats;

namespace SpriteSheetPacker {
    class Program{
        private static string _status;
        private static SpriteSheetPack.SpriteSheetPacker _spriteSheetPacker;
        private static UserSettings _userSettings;
        private static ExportFileTypeFactory _exportFileFactory;

        static void Main(string[] args){
            _spriteSheetPacker = new SpriteSheetPack.SpriteSheetPacker(new SquareFrameListCombiner());
            _userSettings = new UserSettings();
            _exportFileFactory = new ExportFileTypeFactory();

            ConsoleKeyInfo cki;
            do {
                Console.Clear();
                Console.WriteLine(_status);
                DisplayMenu();
                cki = Console.ReadKey(false);
                HandleOption(cki.KeyChar);
            } while (cki.Key != ConsoleKey.Escape && cki.Key != ConsoleKey.Q);
        }

        private static void DisplayMenu() {
            var defaultColor = Console.ForegroundColor;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sprite sheet packer");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-------------------------");
            Console.Write("Exporting files as: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(_userSettings.ExportFileType);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Options:");
            Console.WriteLine("1. Combine all images in ONE folder to spritesheet");
            Console.WriteLine("2. Combine all images in all subfolders of entered path");
            Console.WriteLine("3. Split an image into all its [X x X] components (e.g. 32x32)");
            Console.WriteLine("4. Set default export filetype (e.g. json, plist");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nPress Escape or q to exit\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter option: ");
            Console.ForegroundColor = defaultColor;
        }

        private static void HandleOption(char option) {
            try {
                switch (option) {
                    case '1':
                        CombineAllInFolder();
                        break;
                    case '2':
                        CombineFromSubFolders();
                        break;
                    case '3':
                        SplitSheet();
                        break;
                    case '4':
                        SetDefaultExportType();
                        break;
                }
            }
            catch (Exception e) {
                _status = e.Message;
            }
        }

        private static void CombineAllInFolder() {
            Console.Write("\n Enter input path: ");
            var inputpath = Console.ReadLine();
            Console.Write("\n Enter output path: ");
            var outputpath = Console.ReadLine();
            _spriteSheetPacker.PackImagesInFolder(inputpath, outputpath, _exportFileFactory.Create(_userSettings.ExportFileType));
            _status = "Created new sheet in " + outputpath;
        }

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
    }
}
