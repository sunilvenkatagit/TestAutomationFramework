using CompassCardTest.Pages;
using NUnit.Framework;

namespace CompassCardTest.Tests
{
    public class HomeTests : BaseTest
    {
        [Test]
        public void UserShoulBeAbleTo_VisitCompassCardWebsite()
        {
            string actualPageTitle = new HomePage().LaunchCompasscardWebsite().GetPageTitle();

            Assert.AreEqual("Compass - Home", actualPageTitle);
        }

        [Test]
        public void Validate_GlobalNotificationMessage()
        {
            string notificationMsg = new HomePage().LaunchCompasscardWebsite().GetNotificationMessage();

            Assert.AreEqual("Ministerial Order on mask use now in effect. View the updated policy at translink.ca/covid19.", notificationMsg, "{Notification message doesn't match.}");
        }
    }
}
