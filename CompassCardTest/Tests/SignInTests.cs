using CompassCardTest.Pages;
using NUnit.Framework;

namespace CompassCardTest.Tests
{
    public class SignInTests : BaseTest
    {
        [Test]
        public void UserShouldBeAbleTo_SignInSignOut()
        {
            string pageTitle = new HomePage().LaunchCompasscardWebsite().GoToSignInPage()
                                                                        .SignInToTheApplication("Preettlqa@gmail.com", "TransLink2020!")
                                                                        .ClickOnSignOut()
                                                                        .GetPageTitle();

            Assert.AreEqual("Compass - Home", pageTitle, "Compass Home page title doesn't match");
        }
    }
}
