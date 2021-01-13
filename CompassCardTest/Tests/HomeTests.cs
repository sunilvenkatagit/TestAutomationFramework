using CompassCardTest.Pages;
using NUnit.Framework;
using System.Threading;

namespace CompassCardTest.Tests
{
    public class HomeTests : BaseTest
    {
        [Test]
        public void UserShoulBeAbleTo_VisitCompassCardWebsite()
        {
            System.Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            string pageTitle = new HomePage().LaunchCompasscardWebsite().GetTitle();
            Assert.AreEqual(pageTitle, "Compass - Home");
        }

        [Test]
        public void Validate_GlobalNotificationMessage()
        {
            System.Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            string notificationMsg = new HomePage().LaunchCompasscardWebsite().GetNotificationMessage();
            Assert.AreEqual(notificationMsg, "Ministerial Order on mask use now in effect. View the updated policy at translink.ca/covid19.", "{Notification message doesn't match.}");
        }
    }
}
