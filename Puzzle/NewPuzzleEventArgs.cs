namespace Puzzle
{
    public class NewPuzzleEventArgs
    {
        public bool LastPieceEmpty { get; set; }
        public int PuzzleSize { get; set; }

        public NewPuzzleEventArgs(bool lastPieceEmpty, int puzzleSize)
        {
            LastPieceEmpty = lastPieceEmpty;
            PuzzleSize = puzzleSize;
        }
    }
}