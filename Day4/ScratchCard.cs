using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day4
{
    internal class ScratchCard
    {
        readonly List<int> _winning;
        readonly List<int> _got;
        readonly int _ID;
        private int _copies;

        public int Copies => _copies;
        public int WinningCount => _got.Where(g => _winning.Any(w => w == g)).Count();
        public double Points
        {
            get
            {
                if (WinningCount == 0) return 0;
                else return Math.Pow(2, WinningCount - 1);
            }
        }

        public ScratchCard(String input)
        {
            _copies = 1;
            String pattern = @"^Card +(\d+): ([\d\s]+) \| ([\d\s]+)$";

            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                _ID = int.Parse(match.Groups[1].Value);

                _winning = [];
                foreach (String number in match.Groups[2].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    _winning.Add(int.Parse(number));
                }

                _got = [];
                foreach (String number in match.Groups[3].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    _got.Add(int.Parse(number));
                }
            }
            else
            {
                throw new Exception("Could not parse the following input : " + input);
            }
        }

        public void Copy()
        {
            _copies++;
        }
        public void Copy(int num)
        {
            _copies += num;
        }
    }
}
