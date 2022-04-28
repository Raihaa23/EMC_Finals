namespace GridManagement
{
    public struct SudokuBoardData
    {
        public int[] UnsolvedData;
        public int[] SolvedData;

        public SudokuBoardData(int[] unsolved, int[] solved) : this()
        {
            UnsolvedData = unsolved;
            SolvedData = solved;
        }
    }
}

