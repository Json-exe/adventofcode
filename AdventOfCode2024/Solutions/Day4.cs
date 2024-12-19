using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions;

internal sealed partial class Day4 : IDay
{
    private readonly List<string> _input = [];
    private readonly List<char> _search = ['X', 'M', 'A', 'S'];

    public void Run()
    {
        var inputText = File.ReadLines("inputs\\day4input1.txt");
        foreach (var line in inputText)
        {
            var mappedLine = string.Join("", line.Trim().Select(MapChar));
            _input.Add(mappedLine);
        }

        var horizonalOccurences = SearchHorizontal(_input);
        var verticalOccurences = SearchVertical();
        var diagonalOccurences = SearchDiagonal();
        Console.WriteLine(horizonalOccurences + verticalOccurences + diagonalOccurences);
    }

    private int SearchVertical()
    {
        var lineLength = _input[0].Length;
        List<string> lines = [];
        for (var i = 0; i < lineLength; i++)
        {
            List<char> verticalLine = [];
            verticalLine.AddRange(_input.Select(line => line[i]));
            lines.Add(string.Join("", verticalLine));
        }

        return SearchHorizontal(lines);
    }

    private int SearchDiagonal()
    {
        List<string> diagonalLines = [];
        var xLength = _input[0].Length;
        var yLength = _input.Count;
        var startX = 0;
        var startY = 0;
        var flip = false;
        do
        {
            List<char> diagonalLine2 = [];
            var y = startY;
            var x = startX;
            diagonalLine2.Add(_input[y][x]);
            while ((!flip && y is not 0) || (flip && x != xLength - 1))
            {
                x++;
                y--;
                diagonalLine2.Add(_input[y][x]);
            }

            if (startY == yLength - 1)
            {
                flip = true;
            }

            if (flip)
            {
                startX++;
            }
            else
            {
                startY++;
            }

            diagonalLines.Add(string.Join("", diagonalLine2));
        } while (startX < xLength);

        startX = xLength - 1;
        startY = 0;
        flip = false;
        do
        {
            List<char> diagonalLine2 = [];
            var y = startY;
            var x = startX;
            diagonalLine2.Add(_input[y][x]);
            while (x != xLength - 1)
            {
                x++;
                y++;
                diagonalLine2.Add(_input[y][x]);
            }

            if (startX == 0)
            {
                flip = true;
                xLength--;
            }

            if (!flip)
            {
                startX--;
            }
            else
            {
                startY++;
            }

            diagonalLines.Add(string.Join("", diagonalLine2));
        } while (startX < xLength);

        return SearchHorizontal(diagonalLines);
    }

    private int SearchHorizontal(List<string> lines)
    {
        var regex = XmasRegex();
        return lines.Sum(line => regex.Matches(line).Count);
    }

    private char MapChar(char c)
    {
        return _search.Contains(c) ? c : '.';
    }

    public void RunPart2()
    {
        
        
        
    }

    [GeneratedRegex("(?=(XMAS))|(?=(SAMX))", RegexOptions.Multiline)]
    private static partial Regex XmasRegex();
}