/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompassCardTest.Pages
{
    public class SearchPage : BasePage
    {
        // {Page Actions}
        public SearchPage Open(bool inMobileView = false)
        {
            try
            {
                if (inMobileView)
                {
                    Step.Log(Status.Info, "<b>Opening the website in a mobile view</b>");
                    Driver.Manage().Window.Size = new Size(375, 812);
                }
                Driver.Manage().Cookies.DeleteAllCookies();
                Driver.Navigate().GoToUrl(Url + "/search"); Thread.Sleep(2000);

                UtilLibrary.PassStep(Driver, Step, "Navigated to Search Page");
                return this;
            }
            catch (Exception ex)
            {
                UtilLibrary.FailStep(Driver, Step, "Failed to navigate to Search Page");
                throw new ArgumentException("Failed to navigate to Search page", ex);
            }
        }
    }
}
*/