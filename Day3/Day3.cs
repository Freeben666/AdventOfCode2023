using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// I programmed this after reading code from Ben Harris (https://benharr.is/). Similitudes are to be expected...
// https://tildegit.org/ben/aoc/src/branch/main/AOC2023/Day03.cs

namespace Day3
{
    public class Day3
    {
        readonly List<Number> _numbers;
        readonly List<Symbol> _symbols;

        public Day3(List<String> input)
        {
            _numbers = [];
            _symbols = [];

            for(int row = 0; row < input.Count; row++)
            {
                for(int col = 0; col < input[row].Length; col++)
                {
                    char c = input[row][col];
                    if (c == '.') continue;

                    if (char.IsDigit(c))
                    {
                        Number n = new();
                        List<int> digits = [];

                        n.Start = new Coord(row,col);
                        digits.Add(c - '0');

                        while(col< input[row].Length-1 && char.IsDigit(input[row][col+1]))
                        {
                            digits.Add(input[row][++col] - '0');
                        }

                        n.End = new Coord(row,col);
                        n.Value = Int32.Parse(String.Concat(digits));
                        _numbers.Add(n);
                    }
                    else
                    {
                        Symbol s = new()
                        {
                            Value = input[row][col],
                            Position = new Coord(row, col)
                        };
                        _symbols.Add(s);
                    }
                }
            }
        }

        public int Part1()
        {
            return _numbers
                .Where(n => _symbols.Any(s =>                   // Search for numbers, relative to symbols
                Math.Abs(s.Position.Row - n.Start.Row) <= 1     // no more than 1 row removed from the symbol
                && s.Position.Col >= n.Start.Col - 1            // number does not start further than 1 column after the column of the symbol
                && s.Position.Col <= n.End.Col + 1))            // number does not end further than 1 column before the column of the symbol
            .Sum(n => n.Value); // Add all this up :-)
        }

        public int Part2()
        {
            return _symbols
                .Where( s=>s.Value == '*')                          // search for all the gears
                .Select(s => _numbers.Where(n =>                    // keep only those adjacent to numbers
                    Math.Abs(s.Position.Row - n.Start.Row) <= 1     // no more than 1 row removed from the number
                    && s.Position.Col >= n.Start.Col - 1            // No further than 1 column before the start of the number
                    && s.Position.Col <= n.End.Col + 1)             // No further than 1 column after the end of the number
                .ToArray()                                          // Get those numbers in an array
                )
            .Where(numbers => numbers.Length == 2)                  // Keep only the case where two numbers are adjacent to the gear
            .Sum(numbers => numbers[0].Value * numbers[1].Value);   // multiply the numbers to get the "gear ratios", then add all of the ratios together

        }

        readonly struct Coord(int row, int col)
        {
            public int Row { get; init; } = row;
            public int Col { get; init; } = col;
        }

        private struct Number
        {
            public int Value { get; set; }
            public Coord Start { get; set; }
            public Coord End { get; set; }
        }

        private struct Symbol
        {
            public char Value { get; set; }
            public Coord Position { get; set; }
        }
    }
}
