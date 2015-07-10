using System.Drawing;
using System.Linq;
using SpriteSheetPacker.SpriteSheetPack;

namespace SpriteSheetPacker.ImageManipulation {
    public class SquareFrameListCombiner : IFrameListCombiner {
        public SpriteSheet Combine(FrameList frameList){
            Bitmap finalImage = null;

            // http://codeincomplete.com/posts/2011/5/7/bin_packing/
            // http://jwezorek.com/2013/01/sprite-packing-in-python/
            // http://www.blackpawn.com/texts/lightmaps/default.html

            var sortedList = frameList.Frames.OrderByDescending(f => f.Height*f.Width);

            Node root = new Node() { Width = 1024, Height = 1024 };

            Node node;
            foreach (var frame in sortedList){
                node = FindNode(root, frame.Width, frame.Height);
                if (node != null){
                    var fit = SplitNode(node, frame.Width, frame.Height);
                    frame.PositionInSheetX = fit.X;
                    frame.PositionInSheetY = fit.Y;
                }
            }

            finalImage = new Bitmap(root.Width, root.Height);

            using (Graphics g = Graphics.FromImage(finalImage)) {
                //set background color
                g.Clear(Color.Transparent);

                foreach (var frame in frameList.Frames) {
                    g.DrawImage(frame.Bitmap, new Rectangle(frame.PositionInSheetX, frame.PositionInSheetY, frame.Width, frame.Height));
                }
            }

            return new SpriteSheet(frameList, finalImage, frameList.Name);
        }

        private Node FindNode(Node root, int width, int height) {
            if (root.Used){
                return FindNode(root.Right, width, height) ?? FindNode(root.Down, width, height);
            }
            else if ((width <= root.Width) && (height <= root.Height))
                return root;
            else
                return null;
        }

        private Node SplitNode(Node node, int width, int height){
            node.Used = true;
            node.Down = new Node() { X = node.X, Y = node.Y + height, Width = node.Width, Height = node.Height - height };
            node.Right = new Node() { X = node.X + width, Y = node.Y, Width = node.Width - width, Height = height };
            return node;
        }
    }
}