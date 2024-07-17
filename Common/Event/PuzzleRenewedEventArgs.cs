namespace Puzzle.Common.Event
{
    public class PuzzleRenewedEventArgs(int puzzleSize, bool lastPieceEmpty)
    {
        public int PuzzleSize { get; set; } = puzzleSize;
        public bool LastPieceEmpty { get; set; } = lastPieceEmpty;
    }
}