// See https://aka.ms/new-console-template for more information

using System.Collections.Generic;
using System.Text.RegularExpressions;

using (var file = File.OpenText("input.txt"))
{
    string? line;
    int sum = 0;
    while ((line = file.ReadLine()) != null)
    {
        var first = firstDigit(line);
        var last = lastDigit(line);

        var number = parseDigit(first)*10 + parseDigit(last);

        sum += number;

        Console.WriteLine(line + " : " + first + " " + last + "==>" + number);
    }

    Console.WriteLine(sum);
}

static string firstDigit(string text)
{
    string digit = "";

    string pattern = @"^[a-zA-Z]*?(\d|one|two|three|four|five|six|seven|eight|nine)";

    var match = Regex.Match(text, pattern);

    if (match.Success)
    {
        digit = match.Groups[1].Value;
    }
    else
    {
        digit = "-1";
    }

    return digit;
}

static string lastDigit(string text)
{
    string digit = "";

    string pattern = @"\w*(\d|one|two|three|four|five|six|seven|eight|nine)[a-zA-Z]*$";

    var match = Regex.Match(text, pattern);

    if (match.Success)
    {
        digit = match.Groups[1].Value;
    }
    else
    {
        digit = "-1";
    }

    return digit;
}

static int parseDigit(string sdigit)
{
    Dictionary<string, int> table = new Dictionary<string, int>
    {
        {"zero", 0},
        {"one", 1 },
        {"two", 2 },
        {"three", 3 },
        {"four", 4 },
        {"five", 5 },
        {"six", 6 },
        {"seven", 7 },
        {"eight", 8 },
        {"nine", 9 }
    };

    if (Int32.TryParse(sdigit, out int idigit))
    {
        return idigit;
    }
    else
    {
        return table[sdigit];
    }
}