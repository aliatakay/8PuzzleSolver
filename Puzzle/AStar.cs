using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Puzzle
{
    public class AStar
    {
        private int[] _targetPuzzle;
        private int _puzzleSize;

        private Node _lastNode;
        private Stopwatch _stopwatch;

        public string Time
        {
            get
            {
                return _stopwatch.Elapsed.Seconds.ToString() + "." + _stopwatch.Elapsed.Milliseconds.ToString() + " sec";
            }
        }

        public int NumberOfNodes { get { return GetNumberOfNodes(); } }

        // Constructor
        public AStar(int[] targetGameField, int gameFieldSize)
        {
            _targetPuzzle = targetGameField;
            _puzzleSize = gameFieldSize;
            _stopwatch = new Stopwatch();
        }

        // AStar Algorithm
        public bool Solve(int[] givenPuzzle)
        {
            StopwatchStart();

            Node currentNode = null;

            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            List<Node> neighbourNodes = new List<Node>();

            Node node = new Node(null, givenPuzzle);
            openList.Add(node);

            while (openList.Count != 0)
            {
                currentNode = GetCheapNode(openList);
                openList.Remove(currentNode);

                if (IsFinalNode(currentNode, _targetPuzzle))
                {
                    break;
                }

                neighbourNodes = GetNeighbourNodes(currentNode);

                if (neighbourNodes.Count > 0)
                {
                    foreach (Node nextNode in neighbourNodes)
                    {
                        Node openNode = null;
                        Node closedNode = null;

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

            _lastNode = currentNode;

            if (currentNode != null && !IsFinalNode(currentNode, _targetPuzzle))
            {
                StopwatchEnd();
                return false;
            }
            else
            {
                StopwatchEnd();
                return true;
            }
        }

        private void StopwatchEnd()
        {
            _stopwatch.Stop();
        }

        private void StopwatchStart()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        internal List<int[]> GetPuzzleCases()
        {
            Node currentNode = _lastNode;
            List<int[]> List = new List<int[]>();

            while (true)
            {
                if (currentNode == null)
                {
                    List<int[]> cases = new List<int[]>();
                    for (int i = List.Count - 1; i > -1; i--)
                    {
                        cases.Add(List[i]);
                    }
                    return cases;
                }

                List.Add(currentNode.GameField);
                currentNode = currentNode.ParentNode;
            }
        }

        private int GetNumberOfNodes()
        {
            int count = 0;
            Node currentNode = _lastNode;

            while (true)
            {
                if (currentNode == null)
                {
                    return count;
                }

                ++count;
                currentNode = currentNode.ParentNode;
            }
        }

        private void UpdateNode(List<Node> list, Node oldNode, Node newNode)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == oldNode)
                {
                    list[i] = newNode;
                    return;
                }
            }

            throw new Exception();
        }

        private Node GetNodeFromList(List<Node> list, Node node)
        {
            foreach (Node currentNode in list)
            {
                if (node.GameField.SequenceEqual(currentNode.GameField))
                {
                    return currentNode;
                }
            }

            return null;
        }

        private List<Node> GetNeighbourNodes(Node currentNode)
        {
            List<Node> nodes = new List<Node>();
            int[,] currentPuzzle = PuzzleExtensions.Make2Dimensional(currentNode.GameField, _puzzleSize);
            Point emptyPiece = GetEmptyPiece(currentPuzzle);

            if (emptyPiece.X < currentPuzzle.GetLength(0) - 1)
            {
                nodes.Add(new Node(currentNode, GetNewPuzzle(currentPuzzle, emptyPiece, new Point(emptyPiece.X + 1, emptyPiece.Y))));
            }

            if (emptyPiece.X > 0)
            {
                nodes.Add(new Node(currentNode, GetNewPuzzle(currentPuzzle, emptyPiece, new Point(emptyPiece.X - 1, emptyPiece.Y))));
            }

            if (emptyPiece.Y < currentPuzzle.GetLength(1) - 1)
            {
                nodes.Add(new Node(currentNode, GetNewPuzzle(currentPuzzle, emptyPiece, new Point(emptyPiece.X, emptyPiece.Y + 1))));
            }

            if (emptyPiece.Y > 0)
            {
                nodes.Add(new Node(currentNode, GetNewPuzzle(currentPuzzle, emptyPiece, new Point(emptyPiece.X, emptyPiece.Y - 1))));
            }

            return nodes;
        }

        private int[] GetNewPuzzle(int[,] currentPuzzle, Point emptyPiece, Point newEmptyPiece)
        {
            int[,] newPuzzle = new int[currentPuzzle.GetLength(0), currentPuzzle.GetLength(1)];

            for (int x = 0; x < currentPuzzle.GetLength(0); x++)
            {
                for (int y = 0; y < currentPuzzle.GetLength(1); y++)
                {
                    newPuzzle[x, y] = currentPuzzle[x, y];
                }
            }

            newPuzzle[emptyPiece.X, emptyPiece.Y] = currentPuzzle[newEmptyPiece.X, newEmptyPiece.Y];
            newPuzzle[newEmptyPiece.X, newEmptyPiece.Y] = currentPuzzle[emptyPiece.X, emptyPiece.Y];

            return PuzzleExtensions.Make1Dimensional(newPuzzle, _puzzleSize);
        }

        private Point GetEmptyPiece(int[,] currentGameField)
        {
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

            throw new Exception();
        }

        private bool IsFinalNode(Node node, int[] targetPuzzle)
        {
            if (node.GameField.SequenceEqual(targetPuzzle))
            {
                return true;
            }

            return false;
        }

        private Node GetCheapNode(List<Node> openList)
        {
            Node node = null;

            foreach (Node currentNode in openList)
            {
                if (node == null)
                {
                    node = currentNode;
                }

                if (currentNode.TotalCost < node.TotalCost)
                {
                    node = currentNode;
                }
            }

            return node;
        }
    }
}