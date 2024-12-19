namespace AdventOfCode2024.Solutions;

internal sealed class Day1 : IDay
{
    private readonly List<int> _leftData = [];
    private readonly List<int> _rightData = [];

    public void Run()
    {
        var inputText = File.ReadLines("inputs\\day1input1.txt");
        foreach (var line in inputText)
        {
            var splittedLine = line.Trim().Split("  ", StringSplitOptions.TrimEntries);
            _leftData.Add(int.Parse(splittedLine[0]));
            _rightData.Add(int.Parse(splittedLine[1]));
        }

        _leftData.Sort();
        _rightData.Sort();
        var totalDistance = _leftData.Select((v, i) => Math.Abs(v - _rightData[i])).Sum();
        Console.WriteLine(totalDistance);
    }

    public void RunPart2()
    {
        var inputText = File.ReadLines("inputs\\day1input2.txt");
        foreach (var line in inputText)
        {
            var splittedLine = line.Trim().Split("  ", StringSplitOptions.TrimEntries);
            _leftData.Add(int.Parse(splittedLine[0]));
            _rightData.Add(int.Parse(splittedLine[1]));
        }

        var similarityScore = 0;
        foreach (var value in _leftData)
        {
            var occurences = _rightData.Count(r => value.Equals(r));
            similarityScore += value * occurences;
        }

        Console.WriteLine(similarityScore);
    }
}