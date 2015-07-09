using System;
using System.IO;

namespace SpriteSheetPacker {
    class Program{
        private static string _status;
        static void Main(string[] args){
            ConsoleKeyInfo cki;

            do {
                Console.Clear();
                Console.WriteLine(_status);
                DisplayMenu();
                cki = Console.ReadKey(false);
                switch (cki.KeyChar) {
                    case '1':
                        CombineFromSubFolders();
                        break;
                    case '2':
                        CombineAllInFolder();
                        break;
                    case '3':
                        SplitSheet();
                        break;
                    case '4':
                        MergeToSheet();
                        break;
                }
            } while (cki.Key != ConsoleKey.Escape && cki.Key != ConsoleKey.Q);


            //Combiner.CombineAllFromSubFolders(@"C:\temp\Sprites\player");
            //Combiner.CombineAllInFolder(@"C:\temp\Sprites\Platformer", @"C:\temp\Sprites\");
            //Combiner.SplitSheet(@"C:\temp\Sprites\items\items32.png");
        }

        private static void DisplayMenu() {
            Console.WriteLine();
            Console.WriteLine("Sprite sheet packer");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Options:");
            Console.WriteLine("1. Combine all images in all subfolders of entered path");
            Console.WriteLine("2. Combine all images in ONE folder to spritesheet");
            Console.WriteLine("3. Split an image into all its 32x32 components");
            Console.WriteLine("4. Combine all images in sub folders and then combine that into a sheet");
            Console.WriteLine("\nPress Escape or q to exit\n");
            Console.Write("Enter option: ");
        }

        private static void CombineFromSubFolders(){
            Console.Write("\n Enter path: ");
            string path = Console.ReadLine();
            _status = "Created new sheet @ " + Combiner.CombineAllFromSubFolders(path);
        }

        private static void CombineAllInFolder() {
            Console.Write("\n Enter input path: ");
            string inputpath = Console.ReadLine();
            Console.Write("\n Enter output path: ");
            string outputpath = Console.ReadLine();
            _status = "Created new sheet @ " + Combiner.CombineAllInFolder(inputpath, outputpath);
        }

        private static void SplitSheet() {
            Console.Write("\n Enter input image path: ");
            string inputpath = Console.ReadLine();
            _status = "Created new folder with images in " + Combiner.SplitSheet(inputpath);
        }

        private static void MergeToSheet() {
            Console.Write("\n Enter path: ");
            string path = Console.ReadLine();
            foreach (var directory in Directory.GetDirectories(path)){
                Combiner.CombineAllFromSubFolders(directory);
            }
            _status = "Created new sheet @ " + Combiner.CombineAllFromSubFoldersVertical(path);
        }
    }
}
