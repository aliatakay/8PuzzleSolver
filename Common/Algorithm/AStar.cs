using Puzzle.Common.Entity;
using Puzzle.Common.Extension;
using System.Diagnostics;
using System.Drawing;

namespace Puzzle.Common.Algorithm
{
    public class AStar
    {
        private Node lastNode;
        private readonly int size;
        private readonly int[] puzzle;
        private readonly Stopwatch watch;

        public string Time
        {
            get
            {
                return this.watch.Elapsed.Seconds.ToString() + "." + this.watch.Elapsed.Milliseconds.ToString() + " sec";
            }
        }

        public int NumberOfNodes { get { return GetNumberOfNodes(); } }

        public AStar(int[] puzzle, int size)
        {
            this.size = size;
            this.puzzle = puzzle;
            this.watch = new Stopwatch();
        }

        public bool Solve(int[] givenPuzzle)
        {
            RestartWatch();

            var currentNode = default(Node);

            var openList = new List<Node>();
            var closedList = new List<Node>();
            var neighbourNodes = new List<Node>();

            var node = new Node(givenPuzzle, null);
            openList.Add(node);

            while (openList.Count != 0)
            {
                currentNode = GetCheapNode(openList);
                openList.Remove(currentNode);

                if (IsFinalNode(currentNode, this.puzzle))
                {
                    break;
                }

                neighbourNodes = GetNeighbourNodes(currentNode);

                if (neighbourNodes.Count > 0)
                {
                    foreach (Node nextNode in neighbourNodes)
                    {
                        var openNode = default(Node);
                        var closedNode = default(Node);

                        openNode = GetNodeFromList(openList, nextNode);
                        closedNode = GetNodeFromList(closedList, nextNode);

                        if (openNode != null)
                        {
                            if (openNode.TotalCost > nextNode.TotalCost)
                            {
                                UpdateNode(openList, openNode, nextNode);
                            }
                        }
                        else if (closedNode != null)
                        {
                            if (closedNode.TotalCost > nextNode.TotalCost)
                            {
                                UpdateNode(closedList, closedNode, nextNode);
                            }
                        }

                        if (openNode == null && closedNode == null)
                        {
                            openList.Add(nextNode);
                        }
                    }

                    closedList.Add(currentNode);
                }
            }

            this.lastNode = currentNode;

            StopWatch();

            return currentNode == null || IsFinalNode(currentNode, this.puzzle);
        }

        private void StopWatch()
        {
            this.watch.Stop();
        }

        private void RestartWatch()
        {
            this.watch.Reset();
            this.watch.Start();
        }

        public List<int[]> GetPuzzleCases()
        {
            var currentNode = this.lastNode;
            var list = new List<int[]>();

            while (true)
            {
                if (currentNode == null)
                {
                    var cases = new List<int[]>();

                    for (int i = list.Count - 1; i > -1; i--)
                    {
                        cases.Add(list[i]);
                    }

                    return cases;
                }

                list.Add(currentNode.Puzzle);
                currentNode = currentNode.Parent;
            }
        }

        private int GetNumberOfNodes()
        {
            var count = 0;
            var currentNode = this.lastNode;

            while (true)
            {
                if (currentNode == null)
                {
                    return count;
                }

                ++count;
                currentNode = currentNode.Parent;
            }
        }

        private static void UpdateNode(List<Node> list, Node oldNode, Node newNode)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == oldNode)
                {
                    list[i] = newNode;
                    break;
                }
            }

            return;
        }

        private static Node? GetNodeFromList(List<Node> list, Node node)
        {
            var result = default(Node);

            foreach (Node currentNode in list)
            {
                if (node.Puzzle.SequenceEqual(currentNode.Puzzle))
                {
                    result = currentNode;
                    break;
                }
            }

            return result;
        }

        private List<Node> GetNeighbourNodes(Node currentNode)
        {
            var nodes = new List<Node>();
            var currentPuzzle = PuzzleExtension.ConvertToDimension2(currentNode.Puzzle, this.size);
            var emptyPiece = GetEmptyPiece(currentPuzzle);

            if (emptyPiece.X < currentPuzzle.GetLength(0) - 1)
            {
                nodes.Add(new Node(GetNewPuzzle(currentPuzzle, emptyPiece, new Point(emptyPiece.X + 1, emptyPiece.Y)), currentNode));
            }

            if (emptyPiece.X > 0)
            {
                nodes.Add(new Node(GetNewPuzzle(currentPuzzle, emptyPiece, new Point(emptyPiece.X - 1, emptyPiece.Y)), currentNode));
            }

            if (emptyPiece.Y < currentPuzzle.GetLength(1) - 1)
            {
                nodes.Add(new Node(GetNewPuzzle(currentPuzzle, emptyPiece, new Point(emptyPiece.X, emptyPiece.Y + 1)), currentNode));
            }

            if (emptyPiece.Y > 0)
            {
                nodes.Add(new Node(GetNewPuzzle(currentPuzzle, emptyPiece, new Point(emptyPiece.X, emptyPiece.Y - 1)), currentNode));
            }

            return nodes;
        }

        private int[] GetNewPuzzle(int[,] currentPuzzle, Point emptyPiece, Point newEmptyPiece)
        {
            var newPuzzle = new int[currentPuzzle.GetLength(0), currentPuzzle.GetLength(1)];

            for (int x = 0; x < currentPuzzle.GetLength(0); x++)
            {
                for (int y = 0; y < currentPuzzle.GetLength(1); y++)
                {
                    newPuzzle[x, y] = currentPuzzle[x, y];
                }
            }

            newPuzzle[emptyPiece.X, emptyPiece.Y] = currentPuzzle[newEmptyPiece.X, newEmptyPiece.Y];
            newPuzzle[newEmptyPiece.X, newEmptyPiece.Y] = currentPuzzle[emptyPiece.X, emptyPiece.Y];

            return PuzzleExtension.ConvertToDimension1(newPuzzle, this.size);
        }

        private static Point GetEmptyPiece(int[,] currentGameField)
        {
            var result = default(Point);

            for (int x = 0; x < currentGameField.GetLength(0); x++)
            {
                for (int y = 0; y < currentGameField.GetLength(1); y++)
                {
                    if (currentGameField[x, y] == 0)
                    {
                        return new Point(x, y);
                    }
                }
            }

            return result;
        }

        private static bool IsFinalNode(Node node, int[] targetPuzzle)
        {
            return node.Puzzle.SequenceEqual(targetPuzzle);
        }

        private static Node? GetCheapNode(List<Node> openList)
        {
            var result = default(Node);

            foreach (var currentNode in openList)
            {
                if (result == null || currentNode.TotalCost < result.TotalCost)
                    result = currentNode;
            }

            return result;
        }
    }
}