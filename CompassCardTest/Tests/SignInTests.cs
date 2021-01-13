using CompassCardTest.Pages;
using NUnit.Framework;
using System.Threading;

namespace CompassCardTest.Tests
{
    public class SignInTests : BaseTest
    {
        SignInPage signInPage;
        MyCardsPage myCardsPage;

        [Test]
        public void UserShouldBeAbleTo_SignInSignOut()
        {
            System.Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            signInPage = new HomePage().LaunchCompasscardWebsite().GoToSignInPage();
            myCardsPage = signInPage.EnterEmailAddress("Preettlqa@gmail.com").EnterPassword("TransLink2020!").SubmitSignIn();
            string pageTitle = myCardsPage.ClickOnSignOut().GetTitle();

            Assert.AreEqual(pageTitle, "Compass - Home", "Compass Home page title doesn't match");
        }
    }
}
