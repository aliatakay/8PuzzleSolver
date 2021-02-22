namespace Puzzle
{
    public class ChangedPieceEventArgs
    {
        public int Index { get; set; }
        public int Value { get; set; }

        public ChangedPieceEventArgs(int index, int value)
        {
            Index = index;
            Value = value;
        }
    }
}