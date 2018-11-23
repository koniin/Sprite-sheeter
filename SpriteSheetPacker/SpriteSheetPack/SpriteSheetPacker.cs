using System.Drawing;
using System.IO;
using System.Linq;
using SpriteSheetPacker.ImageManipulation;
using SpriteSheetPacker.MappingFileFormats;

namespace SpriteSheetPacker.SpriteSheetPack {
    public class SpriteSheetPacker {
        private readonly FrameListLoader _loader;
        private readonly IFrameListCombiner _combiner;
        private readonly ImageWriter _writer;
        private readonly MappingFileWriter _mappingWriter;
        private readonly ImageSplitter _imageSplitter;

        public SpriteSheetPacker(IFrameListCombiner frameListCombiner) {
            _combiner = frameListCombiner;
            _loader = new FrameListLoader();
            _writer = new ImageWriter();
            _mappingWriter = new MappingFileWriter();
            _imageSplitter = new ImageSplitter();
        }

        public SpriteSheet PackImagesInFolder(string inputpath, string outputpath, IMappingFile mappingFile, string name = null) {
            using (var frameList = _loader.Load(inputpath)) {
                var spriteSheet = _combiner.Combine(frameList);
                if (name != null) {
                    spriteSheet.Name = name;
                }
                _writer.Write(outputpath, spriteSheet);
                _mappingWriter.Write(outputpath, spriteSheet, mappingFile);
                return spriteSheet;
            }
        }

        public SpriteSheet PackImagesFromSubfolders(string path, IMappingFile mappingFile) {
            var frameListsFromFolders = Directory.GetDirectories(path).SelectMany(d => _loader.Load(d).Frames);
            var spriteSheet = _combiner.Combine(new FrameList() { Frames = frameListsFromFolders.ToList(), Name = new DirectoryInfo(path).Name });
            _writer.Write(path, spriteSheet);
            _mappingWriter.Write(path, spriteSheet, mappingFile);
            return spriteSheet;
        }

        public string SplitImage(string inputpath, int spriteSize) {
            var spriteList = _imageSplitter.Split(inputpath, spriteSize);

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

        public void MakeBlackWhiteCopies(string inputpath) {
            var directoryInfo = new DirectoryInfo(inputpath);
            
            foreach (var file in directoryInfo.EnumerateFiles()) {
                var filePath = Path.Combine(directoryInfo.FullName, file.Name);
                var fileInfo = new FileInfo(filePath);
                
                var blackFileName = Path.GetFileNameWithoutExtension(fileInfo.FullName) + "_b";
                var blackPath = Path.Combine(directoryInfo.FullName, blackFileName + fileInfo.Extension);
                SolidColorCopier.ChangeAllColorsTo(filePath, blackPath, Color.Black);
                
                var whiteFileName = Path.GetFileNameWithoutExtension(fileInfo.FullName) + "_w";
                var whitePath = Path.Combine(directoryInfo.FullName, whiteFileName + fileInfo.Extension);
                SolidColorCopier.ChangeAllColorsTo(filePath, whitePath, Color.White);
            }
        }

        private static string GetFileName(int i, string extension) {
            return (i + extension).PadLeft(7, '0');
        }
    }
}
