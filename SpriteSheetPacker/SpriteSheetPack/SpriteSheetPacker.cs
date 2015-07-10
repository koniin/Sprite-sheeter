using System.Drawing;
using System.IO;
using System.Linq;
using SpriteSheetPacker.ImageManipulation;
using SpriteSheetPacker.MappingFileFormats;

namespace SpriteSheetPacker.SpriteSheetPack {
    public class SpriteSheetPacker {
        private static FrameListLoader loader;
        private static IFrameListCombiner combiner;
        private static ImageWriter writer;
        private static MappingFileWriter mappingWriter;
        private static ImageSplitter imageSplitter;

        public SpriteSheetPacker(IFrameListCombiner frameListCombiner) {
            combiner = frameListCombiner;
            loader = new FrameListLoader();
            writer = new ImageWriter();
            mappingWriter = new MappingFileWriter();
            imageSplitter = new ImageSplitter();
        }

        public void PackImagesInFolder(string inputpath, string outputpath, IMappingFile mappingFile) {
            var frameList = loader.Load(inputpath);
            var spriteSheet = combiner.Combine(frameList);
            writer.Write(outputpath, spriteSheet);
            mappingWriter.Write(outputpath, spriteSheet, mappingFile);
        }

        public void PackImagesFromSubfolders(string path, IMappingFile mappingFile) {
            var frameListsFromFolders = Directory.GetDirectories(path).SelectMany(d => loader.Load(d).Frames);
            var spriteSheet = combiner.Combine(new FrameList() { Frames = frameListsFromFolders.ToList(), Name = new DirectoryInfo(path).Name });
            writer.Write(path, spriteSheet);
            mappingWriter.Write(path, spriteSheet, mappingFile);
        }

        public string SplitImage(string inputpath, int spriteSize) {
            var spriteList = imageSplitter.Split(inputpath, spriteSize);

            var fileInfo = new FileInfo(inputpath);
            var sprites = spriteList as Bitmap[] ?? spriteList.ToArray();
            var newPath = Path.Combine(fileInfo.DirectoryName, Path.GetFileNameWithoutExtension(inputpath));
            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            for (int i = 0; i < sprites.Count(); i++) {
                string filename = GetFileName(i, fileInfo.Extension);
                sprites[i].Save(Path.Combine(newPath, filename));
            }
            return newPath;
        }

        private static string GetFileName(int i, string extension) {
            return (i + extension).PadLeft(7, '0');
        }
    }
}
