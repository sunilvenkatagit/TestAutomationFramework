using AutomationFramework.Configuration.DriverConfig;
using AutomationFramework.Configuration.ReportConfig;
using AutomationFramework.Libraries;
using OpenQA.Selenium;
using static AutomationFramework.Libraries.Enums;

namespace CompassCardTest.Pages
{
    public class HomePage : BasePage
    {
        readonly UIActionsLibrary actionLib = new UIActionsLibrary();
        private readonly By btnSignIn = By.Id("Content_lbSignIn");
        private readonly By notificationMsg = By.XPath("//div[@class='global-message  notification with-btn' and @style='display: block;']/p");

        public HomePage LaunchCompasscardWebsite()
        {
            driver.Navigate().GoToUrl(URL);
            ExtentLogger.Pass("Launched CompassCard", true);
            return this;
        }

        public string GetPageTitle()
        {
            return DriverManager.GetDriver().Title;
        }

        public SignInPage GoToSignInPage()
        {
            actionLib.ClickOnElement(btnSignIn, WaitStrategy.CLICKABLE, "SignIn button");
            return new SignInPage();
        }

        public string GetNotificationMessage()
        {
            return actionLib.GetText(notificationMsg, WaitStrategy.VISIBLE, "Notification popup");
        }
    }
}
