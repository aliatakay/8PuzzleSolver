using Puzzle.Common.Extension;

namespace Puzzle.Common.Entity
{
    public class Node
    {
        public Node? Parent { get; set; }
        public int TotalCost { get; set; }
        public int DepthCost { get; private set; }
        public int[] Puzzle { get; private set; }
        public static int Cost { get { return 1; } }

        public Node(int[] puzzle, Node? parent)
        {
            this.Puzzle = puzzle;
            this.Parent = parent;

            this.TotalCost = 0;
            this.DepthCost = 0;

            if (parent != null)
            {
                this.DepthCost = parent.DepthCost + Cost;
                this.TotalCost = this.DepthCost + PuzzleExtension.GetHeuristicCost(puzzle);
            }
        }
    }
}