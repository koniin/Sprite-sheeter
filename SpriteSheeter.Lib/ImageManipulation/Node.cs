namespace SpriteSheeter.Lib.ImageManipulation{
    public class Node {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Used { get; set; }

        public Node Right { get; set; }
        public Node Down { get; set; }
    }
}