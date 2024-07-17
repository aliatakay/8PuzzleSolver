namespace Puzzle.Common.Extension
{
    public static class PuzzleExtension
    {
        public static int[] ConvertToDimension1(int[,] puzzle, int size)
        {
            var index = 0;
            var result = new int[size * size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    result[index] = puzzle[x, y];
                    ++index;
                }
            }

            return result;
        }

        public static int[,] ConvertToDimension2(int[] puzzle, int size)
        {
            var index = 0;
            var result = new int[size, size];

            for (int x = 0; x < result.GetLength(0); x++)
            {
                for (int y = 0; y < result.GetLength(0); y++)
                {
                    result[x, y] = puzzle[index];
                    ++index;
                }
            }

            return result;
        }

        public static int GetHeuristicCost(int[] puzzle)
        {
            var result = 0;
            
            var board = (int)Math.Sqrt(puzzle.Length);

            for (int i = 0; i < puzzle.Length; i++)
            {
                var value = puzzle[i] - 1 == -2 ? puzzle.Length - 1 : puzzle[i] - 1;

                if (value != i)
                {
                    var idealX = value % board;
                    var idealY = value / board;

                    var currentX = i % board;
                    var currentY = i / board;

                    result += Math.Abs(idealY - currentY) + Math.Abs(idealX - currentX);
                }
            }

            return result;
        }
    }
}