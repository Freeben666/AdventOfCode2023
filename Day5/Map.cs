using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    internal class Map
    {
        readonly MapType _type;
        List<Range> _ranges = [];

        Boolean IsInSrcRange(uint x)
        {
            return _ranges
                .Any(r => r.SourceStart <= x
                && (r.SourceStart + r.Length - 1) >= x
            );
        }

        public uint Convert(uint x)
        {
            if (!IsInSrcRange(x))
            {
                return x;
            }
            else
            {
                Range range = _ranges
                    .Where(r => r.SourceStart <= x
                    && (r.SourceStart + r.Length - 1) >= x)
                    .First();

                return x + (range.DestStart - range.SourceStart);
            }
        }

        public Map(MapType type) { _type = type; }

        public void AddRange(Range range) { _ranges.Add(range); }
        public void AddRange(uint src, uint dest, uint len)
        {
            var r = new Range()
            {
                SourceStart = src,
                DestStart = dest,
                Length = len
            };
            this._ranges.Add(r);
        }

        public enum MapType
        {
            SeedToSoil,
            SoilToFertilizer,
            FertilizerToWater,
            WaterToLight,
            LightToTemperature,
            TemperatureToHumidity,
            HumidityToLocation
        };
        public struct Range
        {
            public uint SourceStart;
            public uint DestStart;
            public uint Length;
        }
    }
}
