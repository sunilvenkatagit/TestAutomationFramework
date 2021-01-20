using AutomationFramework.Configuration.DriverConfig;
using AutomationFramework.Libraries;
using OpenQA.Selenium;
using static AutomationFramework.Libraries.EnumLibrary;

namespace CompassCardTest.Pages
{
    public class SignInPage : BasePage
    {
        readonly ActionsLibrary actionLib = new ActionsLibrary();

        private readonly By txtBoxEmail = By.Id("Content_emailInfo_txtEmail");
        private readonly By txtBoxPassword = By.XPath("//input[@id='Content_passwordInfo_txtPassword' and @type='password']");
        private readonly By btnSignIn = By.Id("Content_btnSignIn"); // JS click

        public string GetTitle()
        {
            return DriverManager.GetDriver().Title;
        }

        public SignInPage EnterEmailAddress(string emailId)
        {
            actionLib.EnterText(txtBoxEmail, emailId, WaitStrategy.PRESENT, "Email Address");
            return this;
        }

        public SignInPage EnterPassword(string password)
        {
            actionLib.EnterText(txtBoxPassword, password, WaitStrategy.PRESENT, "Password");
            return this;
        }

        public MyCardsPage SubmitSignIn()
        {
            actionLib.ClickUsingJavaScript(btnSignIn, WaitStrategy.CLICKABLE, "SignIn button");
            return new MyCardsPage();
        }
    }
}
