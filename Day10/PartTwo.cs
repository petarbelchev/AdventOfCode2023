namespace Day10;

class PartTwo
{
    enum Direction
    {
        North,
        South,
        West,
        East
    }

    private record Pipe(Direction First, Direction Second);

    private Dictionary<char, Pipe> _pipes = new()
    {
        { '|', new Pipe(Direction.North, Direction.South) },
        { '-', new Pipe(Direction.East, Direction.West) },
        { 'L', new Pipe(Direction.North, Direction.East) },
        { 'J', new Pipe(Direction.North, Direction.West) },
        { '7', new Pipe(Direction.South, Direction.West) },
        { 'F', new Pipe(Direction.South, Direction.East) },
    };
    private (int Row, int Col) _start;
    private char[,] _sketch;
    private bool[,] _loop;

    public int GetResult(string[] input)
    {
        DrawSketch(input);

        bool canGoEast = FindDirection(position: Direction.West, _start.Row, _start.Col + 1);
        bool canGoSouth = FindDirection(position: Direction.North, _start.Row + 1, _start.Col);
        bool canGoWest = FindDirection(position: Direction.East, _start.Row, _start.Col - 1);
        bool canGoNorth = FindDirection(position: Direction.South, _start.Row - 1, _start.Col);

        if (canGoNorth && canGoSouth)
        {
            _sketch[_start.Row, _start.Col] = '|';
            Go(Direction.South, _start.Row - 1, _start.Col);
        }
        else if (canGoEast && canGoWest)
        {
            _sketch[_start.Row, _start.Col] = '-';
            Go(Direction.West, _start.Row, _start.Col + 1);
        }
        else if (canGoNorth && canGoEast)
        {
            _sketch[_start.Row, _start.Col] = 'L';
            Go(Direction.South, _start.Row - 1, _start.Col);
        }
        else if (canGoNorth && canGoWest)
        {
            _sketch[_start.Row, _start.Col] = 'J';
            Go(Direction.South, _start.Row - 1, _start.Col);
        }
        else if (canGoSouth && canGoWest)
        {
            _sketch[_start.Row, _start.Col] = '7';
            Go(Direction.North, _start.Row + 1, _start.Col);
        }
        else if (canGoSouth && canGoEast)
        {
            _sketch[_start.Row, _start.Col] = 'F';
            Go(Direction.North, _start.Row + 1, _start.Col);
        }

        int count = FindEnclosedCount();

        return count;
    }

    private void DrawSketch(string[] input)
    {
        _sketch = new char[input.GetLength(0), input[0].Length];
        _loop = new bool[input.GetLength(0), input[0].Length];

        for (int row = 0; row < _sketch.GetLength(0); row++)
        {
            string line = input[row];

            for (int col = 0; col < _sketch.GetLength(1); col++)
            {
                _sketch[row, col] = line[col];

                if (line[col] == 'S')
                {
                    _start.Row = row;
                    _start.Col = col;
                }
            }
        }
    }

    private bool FindDirection(Direction position, int row, int col)
    {
        if (ValidCoordinates(row, col) && _pipes.TryGetValue(_sketch[row, col], out Pipe pipe))
            return pipe.First == position || pipe.Second == position;

        return false;
    }

    private bool ValidCoordinates(int row, int col)
        => row >= 0 && row < _sketch.GetLength(0) && col >= 0 && col < _sketch.GetLength(1);

    private void Go(Direction from, int row, int col)
    {
        while (row != _start.Row || col != _start.Col)
        {
            _loop[row, col] = true;
            Pipe pipe = _pipes[_sketch[row, col]];
            Direction to = pipe.First == from ? pipe.Second : pipe.First;

            if (to == Direction.North)
            {
                from = Direction.South;
                row--;
            }
            else if (to == Direction.East)
            {
                from = Direction.West;
                col++;
            }
            else if (to == Direction.South)
            {
                from = Direction.North;
                row++;
            }
            else
            {
                from = Direction.East;
                col--;
            }
        }

        _loop[row, col] = true;
    }

    private int FindEnclosedCount()
    {
        int count = 0;

        for (int row = 0; row < _loop.GetLength(0); row++)
        {
            for (int col = 0; col < _loop.GetLength(1); col++)
            {
                if (_loop[row, col])
                    continue;
        
                var pipesLeft = new Dictionary<char, int>();

                for (int leftCol = col - 1; leftCol >= 0; leftCol--)
                {
                    if (!_loop[row, leftCol])
                        continue;
                
                    if (!pipesLeft.ContainsKey(_sketch[row, leftCol]))
                        pipesLeft[_sketch[row, leftCol]] = 0;
                    
                    pipesLeft[_sketch[row, leftCol]]++;
                }

                int xCount = pipesLeft.ContainsKey('|') ? pipesLeft['|'] : 0;
                
                if (pipesLeft.TryGetValue('L', out int pipeL) && pipesLeft.TryGetValue('7', out int pipe7))
                    xCount += Math.Min(pipeL, pipe7);
                
                if (pipesLeft.TryGetValue('F', out int pipeF) && pipesLeft.TryGetValue('J', out int pipeJ))
                    xCount += Math.Min(pipeF, pipeJ);
                
                if (xCount % 2 != 0)
                    count++;
            }
        }

        return count;
    }
}
