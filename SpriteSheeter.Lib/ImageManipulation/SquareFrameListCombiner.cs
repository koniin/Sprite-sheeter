using System;
using System.Drawing;
using System.Linq;
using SpriteSheeter.Lib.SpriteSheetPack;

namespace SpriteSheeter.Lib.ImageManipulation {
    public class SquareFrameListCombiner : IFrameListCombiner {
        private Node _root;

        public SpriteSheet Combine(FrameList frameList){
            // http://codeincomplete.com/posts/2011/5/7/bin_packing/
            // http://jwezorek.com/2013/01/sprite-packing-in-python/
            // http://www.blackpawn.com/texts/lightmaps/default.html

            var sortedList = frameList.Frames.OrderByDescending(f => f.Height*f.Width);

            _root = new Node() { Width = sortedList.First().Width, Height = sortedList.First().Height };

            foreach (var frame in sortedList){
                var node = FindNode(_root, frame.Width, frame.Height);
                if (node != null){
                    var fit = SplitNode(node, frame.Width, frame.Height);
                    frame.PositionInSheetX = fit.X;
                    frame.PositionInSheetY = fit.Y;
                }
                else{
                    var fit = GrowNode(frame.Width, frame.Height);
                    frame.PositionInSheetX = fit.X;
                    frame.PositionInSheetY = fit.Y;
                }
            }

            var finalImage = new Bitmap(_root.Width, _root.Height);

            using (var g = Graphics.FromImage(finalImage)) {
                //set background color
                g.Clear(Color.Transparent);

                foreach (var frame in frameList.Frames) {
                    g.DrawImage(frame.Bitmap, new Rectangle(frame.PositionInSheetX, frame.PositionInSheetY, frame.Width, frame.Height));
                }
            }

            return new SpriteSheet(frameList, finalImage, frameList.Name);
        }

        private Node FindNode(Node root, int width, int height){
            if (root.Used){
                return FindNode(root.Right, width, height) ?? FindNode(root.Down, width, height);
            }
            if ((width <= root.Width) && (height <= root.Height))
                return root;
            return null;
        }

        private Node SplitNode(Node node, int width, int height){
            node.Used = true;
            node.Down = new Node() { X = node.X, Y = node.Y + height, Width = node.Width, Height = node.Height - height };
            node.Right = new Node() { X = node.X + width, Y = node.Y, Width = node.Width - width, Height = height };
            return node;
        }

        private Node GrowNode(int width, int height) {
            var canGrowDown = (width <= _root.Width);
            var canGrowRight = (height <= _root.Height);

            var shouldGrowRight = canGrowRight && (_root.Height >= (_root.Width + width)); // attempt to keep square-ish by growing right when height is much greater than width
            var shouldGrowDown = canGrowDown && (_root.Width >= (_root.Height + height)); // attempt to keep square-ish by growing down  when width  is much greater than height

            if (shouldGrowRight)
                return GrowRight(width, height);
            if (shouldGrowDown)
                return GrowDown(width, height);
            if (canGrowRight)
                return GrowRight(width, height);
            if (canGrowDown)
                return GrowDown(width, height);

            // need to ensure sensible root starting size to avoid this happening
            throw new Exception("root starting size error");
        }

        private Node GrowDown(int width, int height){
            _root = new Node() { Used = true, X = 0, Y = 0, Width =  _root.Width, Height = _root.Height + height, 
                Down = new Node() { X = 0, Y = _root.Height, Width = _root.Width, Height = height },
                Right = new Node() { Down = _root.Down, Height = _root.Height, Right = _root.Right, Used = _root.Used, Width = _root.Width, X = _root.X, Y = _root.Y }
            };

            var node = FindNode(_root, width, height);
            return node != null ? SplitNode(node, width, height) : null;
        }

        private Node GrowRight(int width, int height){
            _root = new Node() {
                Used = true, X = 0, Y = 0, Width = _root.Width + width, Height = _root.Height,
                Down = new Node() { Down = _root.Down, Height = _root.Height, Right = _root.Right, Used = _root.Used, Width = _root.Width, X = _root.X, Y = _root.Y },
                Right = new Node() { X = _root.Width, Y = 0, Width = width, Height = _root.Height }
            };

            var node = FindNode(_root, width, height);
            return node != null ? SplitNode(node, width, height) : null;
        }
    }
}