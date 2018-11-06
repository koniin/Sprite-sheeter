using SpriteSheetPacker.SpriteSheetPack;
using System;
using System.Text;

namespace SpriteSheetPacker.MappingFileFormats {
    /*
     {
	    "image": "sprite_sheet.png",
	    "frameCount": 1,
	    "frames": [{
		    "name": "player_1.png",
		    "id": 0,
		    "x": 0,
		    "y": 0,
		    "width": 16,
		    "height": 16
	    }]
    }
    */

    public class SimpleJSON : IMappingFile {
        private StringBuilder _sb;
        private int _frameCount = 0;
        
        public string Extension => ".json";

        public void Format(string imageFileName, SpriteSheet spriteSheet) {
            int frameCount = spriteSheet.FrameList.Frames.Count;

            _sb = new StringBuilder();
            _sb.AppendLine("{");
            _sb.AppendFormat("    \"image\":\"{0}\", {1}", imageFileName, Environment.NewLine);
            _sb.AppendFormat("    \"frameCount\":{0}, {1}", frameCount.ToString(), Environment.NewLine);
            _sb.AppendLine("    \"frames\": [");
            foreach(var frame in spriteSheet.FrameList.Frames) {
                _sb.Append("        { ");
                AddFrame(frame.FileName, frame.PositionInSheetX, frame.PositionInSheetY, frame.Width, frame.Height);
                _sb.Append(" }");
                if (_frameCount < frameCount) {
                    _sb.Append(",");
                }
                _sb.Append(Environment.NewLine);
            }

            _sb.AppendLine("    ]" + Environment.NewLine + "}");
        }

        public string GetFileContent() {
            return _sb.ToString();
        }
        
        private void AddFrame(string fileName, int x, int y, int width, int height) {
            var line = $"\"name\": \"{fileName}\", \"id\": {_frameCount++}, \"x\":{x}, \"y\":{y}, \"width\":{width}, \"height\":{height}";
            _sb.Append(line);
        }
    }
}
