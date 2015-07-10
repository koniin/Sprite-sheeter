using System.IO;
using System.Linq;

namespace SpriteSheetPacker.SpriteSheetPack {
    public class FrameListLoader {
        public FrameList Load(string folder) {
            var files = Directory.GetFiles(folder).Where(file => file.EndsWith(".png")).ToList();
            files.Sort();

            var frameList = new FrameList();
            foreach (var file in files) {
                frameList.AddFrame(file);
            }

            frameList.Name = new DirectoryInfo(folder).Name;
            return frameList;
        }
    }
}
