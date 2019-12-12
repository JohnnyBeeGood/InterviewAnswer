using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SearchResults
    {
        public List<Shirt> Shirts { get; set; }


        public List<SizeCount> SizeCounts { get; set; }


        public List<ColorCount> ColorCounts { get; set; }
    }


    public class SizeCount
    {
        public Size Size { get; set; }

        public int Count { get; set; }
    }


    public class ColorCount: IEquatable<ColorCount>
    {
        public Color Color { get; set; }

        public int Count { get; set; }

        public bool Equals(ColorCount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Color, other.Color);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ColorCount) obj);
        }

        public override int GetHashCode()
        {
            return (Color != null ? Color.GetHashCode() : 0);
        }
    }
}