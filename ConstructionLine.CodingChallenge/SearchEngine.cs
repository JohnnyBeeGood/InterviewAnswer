using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

        }




        public SearchResults Search(SearchOptions options)
        {
            List<Shirt> foundShirts = _shirts.Where(x => (options.Colors.Contains(x.Color) || options.Colors.Count == 0) && (options.Sizes.Contains(x.Size) || options.Sizes.Count == 0)).ToList();


            var colourCounts = Color.All.Select(c => new ColorCount
            {
                Color = c,
                Count = foundShirts.Where(x=> x.Color.Id == c.Id).Count(),
            }).ToList();


            var sizeCount = Size.All.Select(c => new SizeCount
            {
                Size = c,
                Count = foundShirts.Where(x => x.Size.Id == c.Id).Count(),
            }).ToList();


       
            return new SearchResults
            { 
                ColorCounts  = colourCounts,
                SizeCounts = sizeCount,
                Shirts = foundShirts
            };
        }
    }
}