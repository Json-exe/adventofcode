using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions;

internal sealed class Day3 : IDay
{
    public void Run()
    {
        var inputText = File.ReadAllText("inputs\\day3input1.txt");
        var regex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)", RegexOptions.Multiline);
        var matches = regex.Matches(inputText);
        var sum = matches.Select(m => m.Groups)
            .Sum(match => int.Parse(match[1].Value) * int.Parse(match[2].Value));
        Console.WriteLine(sum);
    }

    public void RunPart2()
    {
        var inputText = File.ReadAllText("inputs\\day3input2.txt");
        var regex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)|(don't\(\))|(do\(\))", RegexOptions.Multiline);
        var matches = regex.Matches(inputText);
        var sum = 0;
        var calculate = true;
        foreach (var match in matches.Select(m => m.Groups))
        {
            if (match[3].Success)
            {
                calculate = false;
                continue;
            }

            if (match[4].Success)
            {
                calculate = true;
                continue;
            }

            if (calculate)
            {
                sum += int.Parse(match[1].Value) * int.Parse(match[2].Value);
            }
        }

        Console.WriteLine(sum);
    }
}