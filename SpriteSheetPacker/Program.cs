using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace SpriteSheetPacker {
    class Program {
        static void Main(string[] args){
            //CombineAllFromSubFolders(@"C:\temp\Sprites\player");
            //CombineAllInFolder(@"C:\temp\Sprites\Platformer", @"C:\temp\Sprites\");
            //SplitSheet(@"C:\temp\Sprites\items\items32.png");
        }

        private static void CombineAllFromSubFolders(string folder) {
            var sprites = Directory.GetDirectories(folder).Select(GetSpriteSheet).ToList();
            Bitmap sheet = ImageCombinator.CombineHorizontal(sprites.ToArray());
            var fileName = new DirectoryInfo(folder).Name + ".png";
            sheet.Save(Path.Combine(folder, fileName), ImageFormat.Png);
        }

        private static Bitmap GetSpriteSheet(string folder){
            var dir = new DirectoryInfo(folder);
            var files = Directory.GetFiles(folder);
            return ImageCombinator.CombineHorizontal(files);
        }

        private static void SplitSheet(string inFile){
            var spriteList = ImageSplitter.Split(inFile, 32);

            var fileInfo = new FileInfo(inFile);
            var sprites = spriteList as Bitmap[] ?? spriteList.ToArray();
            var newPath = Path.Combine(fileInfo.DirectoryName, Path.GetFileNameWithoutExtension(inFile));
            if (Directory.Exists(newPath))
                Directory.Delete(newPath, true);

            Directory.CreateDirectory(newPath);

            for (int i = 0; i < sprites.Count(); i++){
                string filename = GetFileName(i, fileInfo.Extension);
                sprites[i].Save(Path.Combine(newPath, filename));
            }
        }

        private static string GetFileName(int i, string extension){
            return (i + extension).PadLeft(7, '0');
        }

        private static void CombineAllInFolder(string folder, string outfolder) {
            var dir = new DirectoryInfo(folder);
            var files = Directory.GetFiles(folder).ToList();
            files.Sort();
            var image = ImageCombinator.CombineHorizontal(files.ToArray());
            image.Save(Path.Combine(outfolder, dir.Name + ".png"), ImageFormat.Png);
        }
    }
}
