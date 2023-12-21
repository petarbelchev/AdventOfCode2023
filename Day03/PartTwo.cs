namespace Day03
{
    public static class PartTwo
    {
        private static char[,] _schematic;
        private static bool[,] _visited;
        private static List<(int row, int col)> _symbols;
        private static List<int> _numbers;
        private static string _number;
        private static int _sum;

        public static int GetResult(string[] input)
        {
            BuildSchematic(input);

            _sum = 0;
            foreach (var symbol in _symbols)
                FindSymbolNumbers(symbol);

            return _sum;
        }

        private static void BuildSchematic(string[] input)
        {
            _schematic = new char[input.Length, input.Length];
            _visited = new bool[input.Length, input.Length];
            _symbols = new();

            for (int row = 0; row < input.Length; row++)
            {
                string line = input[row];

                for (int col = 0; col < line.Length; col++)
                {
                    _schematic[row, col] = line[col];

                    if (IsPossibleGear(line[col]))
                        _symbols.Add((row, col));
                }
            }
        }

        private static bool IsPossibleGear(char ch) => ch == '*';

        private static void FindSymbolNumbers((int row, int col) symbol)
        {
            _numbers = new();
            FindNumber(symbol.row - 1, symbol.col - 1); // Top Left
            FindNumber(symbol.row - 1, symbol.col); // Top
            FindNumber(symbol.row - 1, symbol.col + 1); // Top Right
            FindNumber(symbol.row, symbol.col - 1); // Left
            FindNumber(symbol.row, symbol.col + 1); // Right
            FindNumber(symbol.row + 1, symbol.col - 1); // Bottom Left
            FindNumber(symbol.row + 1, symbol.col); // Bottom
            FindNumber(symbol.row + 1, symbol.col + 1); // Bottom Right

            if (_numbers.Count == 2)
                _sum += _numbers[0] * _numbers[1];
        }

        private static void FindNumber(int row, int col)
        {
            if (InvalidIndex(row) || InvalidIndex(col) || _visited[row, col])
                return;

            _visited[row, col] = true;

            if (char.IsDigit(_schematic[row, col]))
            {
                _number = _schematic[row, col].ToString();
                GoLeft(row, col);
                GoRight(row, col);

                _numbers.Add(int.Parse(_number));
            }
        }

        private static void GoLeft(int row, int col)
        {
            int newCol = col - 1;

            if (InvalidIndex(row) || InvalidIndex(newCol) || _visited[row, newCol])
                return;

            _visited[row, newCol] = true;

            if (char.IsDigit(_schematic[row, newCol]))
            {
                _number = _schematic[row, newCol] + _number;
                GoLeft(row, newCol);
            }

            return;
        }

        private static void GoRight(int row, int col)
        {
            int newCol = col + 1;

            if (InvalidIndex(row) || InvalidIndex(newCol) || _visited[row, newCol])
                return;

            _visited[row, newCol] = true;

            if (char.IsDigit(_schematic[row, newCol]))
            {
                _number += _schematic[row, newCol];
                GoRight(row, newCol);
            }

            return;
        }

        private static bool InvalidIndex(int index) => index < 0 || index >= _schematic.GetLength(0);
    }
}
