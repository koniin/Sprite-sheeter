using System;
using SpriteSheetPacker.ImageManipulation;
using SpriteSheetPacker.MappingFileFormats;

namespace SpriteSheetPacker {
    class Program{
        private static string _status;
        private static SpriteSheetPack.SpriteSheetPacker _spriteSheetPacker;

        static void Main(string[] args){
            _spriteSheetPacker = new SpriteSheetPack.SpriteSheetPacker(new SquareFrameListCombiner());
            
            ConsoleKeyInfo cki;
            do {
                Console.Clear();
                Console.WriteLine(_status);
                DisplayMenu();
                cki = Console.ReadKey(false);
                switch (cki.KeyChar) {
                    case '1':
                        CombineAllInFolder();
                        break;
                    case '2':
                        CombineFromSubFolders();
                        break;
                    case '3':
                        SplitSheet();
                        break;
                }
            } while (cki.Key != ConsoleKey.Escape && cki.Key != ConsoleKey.Q);
        }

        private static void DisplayMenu() {
            Console.WriteLine();
            Console.WriteLine("Sprite sheet packer");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Options:");
            Console.WriteLine("1. Combine all images in ONE folder to spritesheet");
            Console.WriteLine("2. Combine all images in all subfolders of entered path");
            Console.WriteLine("3. Split an image into all its [X x X] components (e.g. 32x32)");
            Console.WriteLine("\nPress Escape or q to exit\n");
            Console.Write("Enter option: ");
        }

        private static void CombineAllInFolder() {
            Console.Write("\n Enter input path: ");
            var inputpath = Console.ReadLine();
            Console.Write("\n Enter output path: ");
            var outputpath = Console.ReadLine();
            _spriteSheetPacker.PackImagesInFolder(inputpath, outputpath, new PList());
            _status = "Created new sheet in " + outputpath;
        }

        private static void CombineFromSubFolders(){
            Console.Write("\n Enter path: ");
            var path = Console.ReadLine();
            _spriteSheetPacker.PackImagesFromSubfolders(path, new PList());
            _status = "Created new sheet @ " + path;
        }

        private static void SplitSheet() {
            Console.Write("\n Enter size: ");
            int size;
            string requestedSize = Console.ReadLine();
            while (!int.TryParse(requestedSize, out size) || size <= 0)
            {
                Console.Write("\n Enter size (number greater than 0): ");
                requestedSize = Console.ReadLine();
            }

            Console.Write("\n Enter input image path: ");
            var inputpath = Console.ReadLine();
            while (!System.IO.File.Exists(inputpath))
            {
                Console.Write("\n Enter input image path: ");
                inputpath = Console.ReadLine();
            }
            var newPath = _spriteSheetPacker.SplitImage(inputpath, size);
            _status = "Created new folder with images in " + newPath;
        }
    }
}
