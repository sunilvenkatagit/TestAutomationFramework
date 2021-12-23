using AutomationFramework.Configuration.DriverConfig;
using AutomationFramework.Libraries;
using OpenQA.Selenium;
using static AutomationFramework.Libraries.Enums;

namespace CompassCardTest.Pages
{
    public class SignInPage : BasePage
    {
        readonly UIActionsLibrary actionLib = new UIActionsLibrary();

        private readonly By email_txtBox = By.Id("Content_emailInfo_txtEmail");
        private readonly By password_txtBox = By.XPath("//input[@id='Content_passwordInfo_txtPassword' and @type='password']");
        private readonly By signIn_btn = By.Id("Content_btnSignIn");

        public string GetTitle()
        {
            return DriverManager.GetDriver().Title;
        }

        public SignInPage EnterEmailAddress(string emailId)
        {
            actionLib.EnterText(email_txtBox, emailId, WaitStrategy.PRESENT, "Email Address");
            return this;
        }

        public SignInPage EnterPassword(string password)
        {
            actionLib.EnterText(password_txtBox, password, WaitStrategy.PRESENT, "Password");
            return this;
        }

        public MyCardsPage SubmitSignIn()
        {
            actionLib.ClickUsingJavaScript(signIn_btn, WaitStrategy.CLICKABLE, "SignIn button");
            return new MyCardsPage();
        }

        public MyCardsPage SignInToTheApplication(string email, string password)
        {
            EnterEmailAddress(email);
            EnterPassword(password);
            SubmitSignIn();
            return new MyCardsPage();
        }
    }
}
