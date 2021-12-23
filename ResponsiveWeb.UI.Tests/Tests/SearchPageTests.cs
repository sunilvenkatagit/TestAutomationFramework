/*using CompassCardTest.Pages;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace CompassCardTest.Tests
{
    [TestFixture, Category("All")]
    [Category("Search")]
    [Category("SearchPage")]
    public class SearchPageTests
    {
        [Test, Category("Smoke")]
        public void St_15003_UserShouldBeAbleTo_SeeAutoSuggestionsFromSearchboxDropdown()
        {
            // Arrange
            string searchTerm = "pro";

            // Act
            var searchPage = new SearchPage().Open();
            var listOfSuggestions = searchPage.EnterTextInSearchBox(searchTerm).ReturnAllDropDownSuggestions();

            // Assert
            Assert.IsNotNull(listOfSuggestions, "Failed to show auto suggestions. Please check!");
            Assert.Multiple(() =>
            {
                Assert.LessOrEqual(listOfSuggestions.Count, 5, "Auto suggestions count is greater than 5 or less than 0. Please check!");
                Assert.That(searchPage.AreAllSuggestionsStartWithSearchTerm(listOfSuggestions, searchTerm), "Suggestion are not relevant to the search keyword. Please check!");
            });
        }
    }
}
*/