using AutomationFramework.Libraries;
using OpenQA.Selenium;
using static AutomationFramework.Libraries.EnumLibrary;

namespace CompassCardTest.Pages
{
    public class MyCardsPage : ActionsLibrary
    {
        private readonly By btnSignOut = By.Id("btnSignOut");

        public HomePage ClickOnSignOut()
        {
            ClickOnElement(btnSignOut, WaitStrategy.CLICKABLE, "SignOut button");
            return new HomePage();
        }
    }
}