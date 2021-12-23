using AutomationFramework.Libraries;
using OpenQA.Selenium;
using static AutomationFramework.Libraries.Enums;

namespace CompassCardTest.Pages
{
    public class MyCardsPage : BasePage
    {
        readonly UIActionsLibrary actionLib = new UIActionsLibrary();
        private readonly By btnSignOut = By.Id("btnSignOut");

        public HomePage ClickOnSignOut()
        {
            actionLib.ClickOnElement(btnSignOut, WaitStrategy.CLICKABLE, "SignOut button");
            return new HomePage();
        }
    }
}