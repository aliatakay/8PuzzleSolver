namespace Puzzle
{
    internal static class PuzzleExtensions
    {
        public static int[,] Make2Dimensional(int[] puzzle, int puzzleSize)
        {
            int[,] changedPuzzle = new int[puzzleSize, puzzleSize];
            int index = 0;

            for (int x = 0; x < changedPuzzle.GetLength(0); x++)
            {
                for (int y = 0; y < changedPuzzle.GetLength(0); y++)
                {
                    changedPuzzle[x, y] = puzzle[index];
                    ++index;
                }
            }

            return changedPuzzle;
        }

        public static int[] Make1Dimensional(int[,] puzzle, int puzzleSize)
        {
            int[] changedPuzzle = new int[puzzleSize * puzzleSize];
            int index = 0;

            for (int x = 0; x < puzzleSize; x++)
            {
                for (int y = 0; y < puzzleSize; y++)
                {
                    changedPuzzle[index] = puzzle[x, y];
                    ++index;
                }
            }

            return changedPuzzle;
        }
    }
}