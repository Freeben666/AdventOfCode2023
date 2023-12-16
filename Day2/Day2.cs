using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day2
{
    enum Colour
    {
        Red,
        Green,
        Blue
    }
    class Cube
    {
        public Colour Colour { get; set; }
        public Cube(Colour colour) { Colour = colour;}
    }
    class Set
    {
        int _red;
        int _green;
        int _blue;
        public int Red => _red;
        public int Green => _green;
        public int Blue => _blue;

        public Set()
        {
            _red = 0;
            _green = 0;
            _blue = 0;
        }
        public Set(int red, int green, int blue)
        {
            _red = red;
            _green = green;
            _blue = blue;
        }
        public Set(String setString)
        {
            foreach( String s in setString.Split(','))
            {
                var trimmed = s.Trim();
                var infos = trimmed.Split(" ");

                int quantity;
                if(!int.TryParse(infos[0], out quantity))
                {
                    throw new Exception("Could not parse quantity in " + trimmed);
                }

                switch(infos[1])
                {
                    case "red":
                        _red += quantity;
                        break;
                    case "green":
                        _green += quantity;
                        break;
                    case "blue":
                        _blue += quantity;
                        break;
                }
            }
        }
        public void Add(Cube cube)
        {
            switch (cube.Colour)
            {
                case Colour.Red:
                    this._red += 1;
                    break;
                case Colour.Green:
                    this._green += 1;
                    break;
                case Colour.Blue:
                    this._blue += 1;
                    break;
            }
        }

        public Boolean Possible(Set constraint)
        {
            if (this.Red > constraint.Red) { return false; }
            else if (this.Green > constraint.Green) { return false; }
            else if (this.Blue > constraint.Blue) { return false; }
            else { return true; }
        }
    }
    internal class Game
    {
        int _ID;
        List<Set> _sets;

        public int ID => _ID;
        public int MaxRed => _sets.Max(t => t.Red);
        public int MaxGreen => _sets.Max(t => t.Green);
        public int MaxBlue => _sets.Max(t => t.Blue);

        public int Power => this.MaxRed * this.MaxGreen * this.MaxBlue;

        public Game()
        {
            _ID = 0;
            _sets = new List<Set>();
        }
        public Game(String gameString) : this()
        {
            String pattern = @"^Game (\d+):";

            Match match = Regex.Match(gameString, pattern);
            if (match.Success)
            {
                _ID = Int32.Parse(match.Groups[1].Value);
            }
            else
            {
                throw new Exception("Could not parse Game ID");
            }

            String pattern2 = @"[:;]\s([\w\s,]+)";

            foreach (Match matchSet in Regex.Matches(gameString, pattern2))
            {
                this.Add(new Set(matchSet.Groups[1].Value));
            }
        }

        public void Add(Set set)
        {
            _sets.Add(set);
        }

        public Boolean Possible(Set constraint)
        {
            foreach(Set set in _sets)
            {
                if(!set.Possible(constraint)) { return false; }
            }
            return true;
        }
    }
}
