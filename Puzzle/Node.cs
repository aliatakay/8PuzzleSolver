using System;

namespace Puzzle
{
    internal class Node
    {
        public Node ParentNode { get; set; }

        public int DepthCost { get; private set; }

        public int[] GameField { get; private set; }

        public int TotalCost { get; set; }

        private const int _cost = 1;

        public static int Cost
        {
            get
            {
                return _cost;
            }
        }

        public Node(Node parentNode, int[] puzzle)
        {
            ParentNode = parentNode;
            GameField = puzzle;

            if (parentNode != null)
            {
                DepthCost = parentNode.DepthCost + Cost;
                TotalCost = DepthCost + CalculateHeuristic(puzzle);
            }
            else
            {
                DepthCost = 0;
                TotalCost = 0;
            }
        }

        private int CalculateHeuristic(int[] GameField)
        {
            return CalculateManhattanCost(GameField);
        }

        private int CalculateManhattanCost(int[] GameField)
        {
            int heuristicCost = 0;
            int gridX = (int)Math.Sqrt(GameField.Length);
            int idealX;
            int idealY;
            int currentX;
            int currentY;
            int value;

            for (int i = 0; i < GameField.Length; i++)
            {
                value = GameField[i] - 1;
                if (value == -2)
                {
                    value = GameField.Length - 1;
                }

                if (value != i)
                {
                    idealX = value % gridX;
                    idealY = value / gridX;

                    currentX = i % gridX;
                    currentY = i / gridX;

                    heuristicCost += (Math.Abs(idealY - currentY) + Math.Abs(idealX - currentX));
                }
            }

            return heuristicCost;
        }
    }
}