namespace Puzzle.Common.Event
{
    public class PieceChangedEventArgs(int index, int value)
    {
        public int Index { get; set; } = index;
        public int Value { get; set; } = value;
    }
}