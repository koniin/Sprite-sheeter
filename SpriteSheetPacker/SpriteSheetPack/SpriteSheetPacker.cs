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

        public void PackImagesInFolder(string inputpath, string outputpath, IMappingFile mappingFile) {
            var frameList = _loader.Load(inputpath);
            var spriteSheet = _combiner.Combine(frameList);
            _writer.Write(outputpath, spriteSheet);
            _mappingWriter.Write(outputpath, spriteSheet, mappingFile);
        }

        public void PackImagesFromSubfolders(string path, IMappingFile mappingFile) {
            var frameListsFromFolders = Directory.GetDirectories(path).SelectMany(d => _loader.Load(d).Frames);
            var spriteSheet = _combiner.Combine(new FrameList() { Frames = frameListsFromFolders.ToList(), Name = new DirectoryInfo(path).Name });
            _writer.Write(path, spriteSheet);
            _mappingWriter.Write(path, spriteSheet, mappingFile);
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

        private static string GetFileName(int i, string extension) {
            return (i + extension).PadLeft(7, '0');
        }
    }
}
