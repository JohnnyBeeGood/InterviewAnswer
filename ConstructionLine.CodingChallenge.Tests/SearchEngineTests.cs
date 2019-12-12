using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Test]
        public void Test()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red},
                Sizes = new List<Size> {Size.Small}
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }


        [Test]
        public void That_The_Correct_Number_Of_All_Colours_Are_Listed()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red)
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions();
            searchOptions.Colors.AddRange(new List<Color> { Color.Red });

            var result = searchEngine.Search(searchOptions);

            Assert.AreEqual(0, result.ColorCounts.FirstOrDefault(c => c.Color.Id.Equals(Color.Black.Id)).Count);
            Assert.AreEqual(0, result.ColorCounts.FirstOrDefault(c => c.Color.Id.Equals(Color.Blue.Id)).Count);
            Assert.AreEqual(3, result.ColorCounts.FirstOrDefault(c => c.Color.Id.Equals(Color.Red.Id)).Count);

            Assert.AreEqual(1, result.SizeCounts.FirstOrDefault(c => c.Size.Id.Equals(Size.Small.Id)).Count);
            Assert.AreEqual(2, result.SizeCounts.FirstOrDefault(c => c.Size.Id.Equals(Size.Medium.Id)).Count);
            Assert.AreEqual(0, result.SizeCounts.FirstOrDefault(c => c.Size.Id.Equals(Size.Large.Id)).Count);
        }

        [Test]
        public void That_The_Correct_Number_Of_All_Sizes_Are_Listed()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red)
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions();
            searchOptions.Sizes.Add(Size.Large);

            var result = searchEngine.Search(searchOptions);

            Assert.AreEqual(0, result.SizeCounts.FirstOrDefault(c => c.Size.Id.Equals(Size.Small.Id)).Count);
            Assert.AreEqual(0, result.SizeCounts.FirstOrDefault(c => c.Size.Id.Equals(Size.Medium.Id)).Count);
            Assert.AreEqual(1, result.SizeCounts.FirstOrDefault(c => c.Size.Id.Equals(Size.Large.Id)).Count);
        }


        [Test]
        public void It_Should_Find_All_Documents_With_No_SearchOptions()
        {
            // Arrange
            var searchOptions = new SearchOptions();

            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue)
            };

            // Act
            var searchEngine = new SearchEngine(shirts);
            var results = searchEngine.Search(searchOptions);

            // Assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void It_Should_Find_A_Single_TShirt()
        {
            // Arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue)
            };

            // Act
            var searchEngine = new SearchEngine(shirts);
            var results = searchEngine.Search(searchOptions);

            // Assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void It_Should_Find_A_Multiple_Sizes_For_A_Given_Color()
        {
            // Arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Large, Color.Red),

                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black)
            };

            // Act
            var searchEngine = new SearchEngine(shirts);
            var results = searchEngine.Search(searchOptions);

            // Assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void It_Should_Find_A_Multiple_Colors_For_A_Given_Size()
        {
            // Arrange
            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Medium }
            };

            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Large, Color.Red),

                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black)
            };

            // Act
            var searchEngine = new SearchEngine(shirts);
            var results = searchEngine.Search(searchOptions);

            // Assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void It_Should_Find_Nothing_With_No_Match()
        {
            // Arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Black }
            };

            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Yellow - Large", Size.Large, Color.Yellow)
            };

            // Act
            var searchEngine = new SearchEngine(shirts);
            var results = searchEngine.Search(searchOptions);

            // Assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void It_Should_Find_Multiple_Identical_Shirts_For_A_Single_Term()
        {
            // Arrange
            var expectedCount = 4;

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red)
            };

            // Act
            var searchEngine = new SearchEngine(shirts);
            var results = searchEngine.Search(searchOptions);

            // Assert
            Assert.AreEqual(results.Shirts.Count, expectedCount);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void It_Should_Find_Handle_Repeated_Size_Options()
        {
            // Arrange
            var expectedCount = 1;

            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Medium, Size.Medium, Size.Medium, Size.Medium }
            };

            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Yellow - Large", Size.Large, Color.Yellow)
            };

            // Act
            var searchEngine = new SearchEngine(shirts);
            var results = searchEngine.Search(searchOptions);

            // Assert
            Assert.AreEqual(results.Shirts.Count, expectedCount);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void It_Should_Find_Handle_Repeated_Color_Options()
        {
            // Arrange
            var expectedCount = 2;

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Red, Color.Red, Color.Red }
            };

            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Yellow - Large", Size.Large, Color.Yellow)
            };

            // Act
            var searchEngine = new SearchEngine(shirts);
            var results = searchEngine.Search(searchOptions);

            // Assert
            Assert.AreEqual(results.Shirts.Count, expectedCount);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

    }
}
