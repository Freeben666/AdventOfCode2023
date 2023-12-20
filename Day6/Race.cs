using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    internal class Race(long t, long r)
    {
        internal static int Count = 0;

        int _ID = Race.Count++;
        long _time = t; // Duration of the race
        long _record = r; // Record distance on that race

        public double Discriminant => Math.Pow(_time, 2) - 4 * _record;
        public Boolean HasSolutions => Discriminant >= 0;
        public (double?, double?) Solutions => HasSolutions ? ((_time - Math.Sqrt(this.Discriminant)) / 2.0, (_time + Math.Sqrt(this.Discriminant)) / 2.0) : (null, null);
        double? MinSolution => HasSolutions ? Math.Min((double)Solutions.Item1, (double)Solutions.Item2) : null;
        double? MaxSolution => HasSolutions ? Math.Max((double)Solutions.Item1, (double)Solutions.Item2) : null;

        long? MinToBeatRecord => HasSolutions ? (long)Math.Ceiling((double)MinSolution) : null;
        long? MaxToBeatRecord => HasSolutions ? (long)Math.Floor((double)MaxSolution) : null;
        public long NumberOfWays => HasSolutions ? (long)MaxToBeatRecord - (long)MinToBeatRecord + 1 : 0;

        public long? Distance(long pushTime)
        {
            if (pushTime < 0 || pushTime > _time)
            {
                return null;
            }
            else
            {
                return (_time - pushTime) * pushTime;
            }
        }

        public void Solve()
        {
            Console.WriteLine( "Game {0}", _ID);
            Console.WriteLine( "Race duration : {0}, Record = {1}", _time, _record );
            Console.WriteLine("Discriminant = {0}", this.Discriminant);

            Console.WriteLine("Solutions : {0} | {1}", MinToBeatRecord, MaxToBeatRecord);
            Console.WriteLine("Ways : {0}", NumberOfWays );
        }
    }
}
