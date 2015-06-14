using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace SpriteSheetPacker{
    public class Combiner{
        public static string CombineAllFromSubFolders(string folder){
            var sprites = Directory.GetDirectories(folder).Select(GetSpriteSheet).ToList();
            Bitmap sheet = ImageCombinator.CombineHorizontal(sprites.ToArray());
            var fileName = new DirectoryInfo(folder).Name + ".png";
            string newFile = Path.Combine(folder, fileName);
            sheet.Save(newFile, ImageFormat.Png);
            return newFile;
        }

        public static string CombineAllFromSubFoldersVertical(string folder) {
            var sprites = Directory.GetDirectories(folder).Select(GetSpriteSheet).ToList();
            Bitmap sheet = ImageCombinator.CombineVertical(sprites.ToArray());
            var fileName = new DirectoryInfo(folder).Name + ".png";
            string newFile = Path.Combine(folder, fileName);
            sheet.Save(newFile, ImageFormat.Png);
            return newFile;
        }

        private static Bitmap GetSpriteSheet(string folder){
            var files = Directory.GetFiles(folder);
            return ImageCombinator.CombineHorizontal(files);
        }

        public static string SplitSheet(string inFile){
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
            return newPath;
        }

        private static string GetFileName(int i, string extension){
            return (i + extension).PadLeft(7, '0');
        }

        public static string CombineAllInFolder(string folder, string outfolder){
            var dir = new DirectoryInfo(folder);
            var files = Directory.GetFiles(folder).ToList();
            files.Sort();
            var image = ImageCombinator.CombineHorizontal(files.ToArray());
            string filename = Path.Combine(outfolder, dir.Name + ".png");
            image.Save(filename, ImageFormat.Png);
            return filename;
        }
    }
}