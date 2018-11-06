using System.Text;
using SpriteSheetPacker.SpriteSheetPack;

namespace SpriteSheetPacker.MappingFileFormats {
    public class PList : IMappingFile {
        private StringBuilder _plist;

        public string Extension { get { return ".plist"; } }

        public void Start() {
            _plist = new StringBuilder();
            _plist.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            _plist.AppendLine("<!DOCTYPE plist PUBLIC \"-//Apple Computer//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">");
            _plist.AppendLine("<plist version=\"1.0\">");
            _plist.AppendLine("	<dict>");
            _plist.AppendLine("		<key>frames</key>");
            _plist.AppendLine("		<dict>");
        }

        public void AddFrame(string fileName, int x, int y, int width, int height) {
            _plist.AppendLine("			<key>" + fileName + "</key>");
            _plist.AppendLine("			<dict>");
            _plist.AppendLine("				<key>frame</key>");
            _plist.AppendLine("				<string>{{" + x + "," + y + "},{" + width + "," + height + "}}</string>");
            _plist.AppendLine("				<key>offset</key>");
            _plist.AppendLine("				<string>{0,0}</string>");
            _plist.AppendLine("				<key>rotated</key>");
            _plist.AppendLine("				<false/>");
            _plist.AppendLine("				<key>sourceColorRect</key>");
            _plist.AppendLine("				<string>{{0,0},{" + width + "," + height + "}}</string>");
            _plist.AppendLine("				<key>sourceSize</key>");
            _plist.AppendLine("				<string>{{" + width + "," + height + "}}</string>");
            _plist.AppendLine("			</dict>");
        }

        public void End(string fileName, int width, int height) {
            _plist.AppendLine("		</dict>");
            _plist.AppendLine("		<key>metadata</key>");
            _plist.AppendLine("		<dict>");
            AppendMetaData(fileName, width, height);
            _plist.AppendLine("		</dict>");
            _plist.AppendLine("	</dict>");
            _plist.AppendLine("</plist>");
        }

        public void AddFrames(FrameList frameList) {
            foreach (var frame in frameList.Frames) {
                AddFrame(frame.FileName, frame.PositionInSheetX, frame.PositionInSheetY, frame.Width, frame.Height);
            }
        }

        public string GetFileContent() {
            return _plist.ToString();
        }

        private void AppendMetaData(string fileName, int width, int height) {
            _plist.AppendLine("            <key>format</key>");
            _plist.AppendLine("            <integer>2</integer>");
            _plist.AppendLine("            <key>realTextureFileName</key>");
            _plist.AppendLine("            <string>" + fileName + "</string>");
            _plist.AppendLine("            <key>size</key>");
            _plist.AppendLine("            <string>{" + width + "," + height + "}</string>");
            _plist.AppendLine("            <key>smartupdate</key>");
            _plist.AppendLine("            <string>-</string>");
            _plist.AppendLine("            <key>textureFileName</key>");
            _plist.AppendLine("            <string>" + fileName + "</string>");
        }
    }
}
