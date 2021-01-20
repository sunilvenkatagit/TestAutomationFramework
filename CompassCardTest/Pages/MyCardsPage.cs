using AutomationFramework.Libraries;
using OpenQA.Selenium;
using static AutomationFramework.Libraries.EnumLibrary;

namespace CompassCardTest.Pages
{
    public class MyCardsPage : BasePage
    {
        readonly ActionsLibrary actionLib = new ActionsLibrary();
        private readonly By btnSignOut = By.Id("btnSignOut");

        public HomePage ClickOnSignOut()
        {
            actionLib.ClickOnElement(btnSignOut, WaitStrategy.CLICKABLE, "SignOut button");
            return new HomePage();
        }
    }
}