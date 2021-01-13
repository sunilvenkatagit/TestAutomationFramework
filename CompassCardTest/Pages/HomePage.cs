using AutomationFramework.Configuration.DriverConfig;
using AutomationFramework.Configuration.ReportConfig;
using AutomationFramework.Libraries;
using OpenQA.Selenium;
using static AutomationFramework.Libraries.EnumLibrary;

namespace CompassCardTest.Pages
{
    public class HomePage : ActionsLibrary
    {
        private readonly By btnSignIn = By.Id("Content_lbSignIn");
        private readonly By notificationMsg = By.XPath("//div[@class='global-message  notification with-btn' and @style='display: block;']/p");

        public HomePage LaunchCompasscardWebsite()
        {
            DriverManager.GetDriver().Url = "https://compasscard.ca";
            ExtentLogger.Pass("Launched CompassCard", true);
            return this;
        }

        public string GetTitle()
        {
            return DriverManager.GetDriver().Title;
        }

        public SignInPage GoToSignInPage()
        {
            ClickOnElement(btnSignIn, WaitStrategy.CLICKABLE, "SignIn button");
            return new SignInPage();
        }

        public string GetNotificationMessage()
        {
            return GetText(notificationMsg, WaitStrategy.VISIBLE, "Notification popup");
        }
    }
}
