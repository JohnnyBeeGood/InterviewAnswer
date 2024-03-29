﻿using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class Size : IEquatable<Size>
    {
        public Guid Id { get; }

        public string Name { get; }

        private Size(Guid id, string name)
        {
            Id = id;
            Name = name;
        }


        public static Size Small = new Size(Guid.NewGuid(), "Small");
        public static Size Medium = new Size(Guid.NewGuid(), "Medium");
        public static Size Large = new Size(Guid.NewGuid(), "Large");


        public static List<Size> All = 
            new List<Size>
            {
                Small,
                Medium,
                Large
            };


        public bool Equals(Size other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Size) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}