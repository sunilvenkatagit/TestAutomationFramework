using static AutomationFramework.Libraries.UtilityLibrary;
using static AutomationFramework.Libraries.EnumLibrary;
using AutomationFramework.Configuration.DriverConfig;
using AutomationFramework.Configuration.ReportConfig;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace AutomationFramework.Libraries
{
    public sealed class ActionsLibrary
    {
        public void ClickOnElement(By element, WaitStrategy waitStrategy, string elementName)
        {
            try
            {
                ExplicitlyWaitFor(element, waitStrategy).Click();
                ExtentLogger.Pass($"Clicked on { elementName }", true);
            }
            catch (Exception)
            {
                ExtentLogger.Fail($"Failed to Click on { elementName }", true);
                throw;
            }
        }

        public void ClickUsingJavaScript(By element, WaitStrategy waitStrategy, string elementName)
        {
            try
            {
                ExplicitlyWaitFor(element, waitStrategy);
                IJavaScriptExecutor js = (IJavaScriptExecutor)DriverManager.GetDriver();
                js.ExecuteScript("arguments[0].click();", GetWebElement(element));
                ExtentLogger.Pass($"Clicked on { elementName }", true);
            }
            catch (Exception)
            {
                ExtentLogger.Fail($"Failed to Click on { elementName }", true);
                throw;
            }
        }

        public void ClickUsingActions(By element, Perform action, string elementName)
        {
            try
            {
                switch (action)
                {
                    case Perform.SINGLECLICK:
                        new Actions(DriverManager.GetDriver())
                            .MoveToElement(ExplicitlyWaitFor(element, WaitStrategy.CLICKABLE)).Click().Build().Perform();
                        break;
                    case Perform.DOUBLECLICK:
                        new Actions(DriverManager.GetDriver())
                            .MoveToElement(ExplicitlyWaitFor(element, WaitStrategy.CLICKABLE)).DoubleClick().Build().Perform();
                        break;
                }
                ExtentLogger.Pass($"Clicked on { elementName }", true);
            }
            catch (Exception)
            {
                ExtentLogger.Fail($"Failed to click on { elementName }", true);
                throw;
            }
        }

        public void EnterText(By element, string text, WaitStrategy waitStrategy, string elementName)
        {
            try
            {
                ExplicitlyWaitFor(element, waitStrategy).SendKeys(Keys.Clear + text);
                ExtentLogger.Pass($"Entered '{ text }' into { elementName } box", true);
            }
            catch (Exception)
            {
                ExtentLogger.Fail($"Failed to enter text into { elementName } box", true);
                throw;
            }
        }

        public string GetText(By element, WaitStrategy waitStrategy, string elementName)
        {
            string text;
            try
            {
                text = ExplicitlyWaitFor(element, waitStrategy).Text;
                HighlightElement(element);
                ExtentLogger.Pass($"Got text from { elementName }", true);
            }
            catch (Exception)
            {
                ExtentLogger.Fail($"Failed to get text from { elementName }", true);
                throw;
            }
            return text;
        }

        public string GetValueByAttribute(By element, WaitStrategy waitStrategy, string attribute, string elementName)
        {
            string attributeValue;
            try
            {
                attributeValue = ExplicitlyWaitFor(element, waitStrategy).GetAttribute(attribute);
                HighlightElement(element);
                ExtentLogger.Pass($"Got property '{ attribute }' from { elementName }", true);
            }
            catch (Exception)
            {
                ExtentLogger.Fail($"Failed to get property '{ attribute }' from { elementName }", true);
                throw;
            }
            return attributeValue;
        }

        public void SelectDropdown(By element, SelectBy selectBy, string value, string elementName)
        {
            try
            {
                switch (selectBy)
                {
                    case SelectBy.INDEX:
                        new SelectElement(ExplicitlyWaitFor(element, WaitStrategy.PRESENT)).SelectByIndex(Convert.ToInt32(value));
                        break;
                    case SelectBy.TEXT:
                        new SelectElement(ExplicitlyWaitFor(element, WaitStrategy.PRESENT)).SelectByText(value);
                        break;
                    case SelectBy.VALUE:
                        new SelectElement(ExplicitlyWaitFor(element, WaitStrategy.PRESENT)).SelectByValue(value);
                        break;
                }
                ExtentLogger.Pass($"Selected '{ new SelectElement(ExplicitlyWaitFor(element, WaitStrategy.PRESENT)).SelectedOption.Text }' from dropdown { elementName }", true);
            }
            catch (Exception)
            {
                ExtentLogger.Fail($"Failed to select { value } from dropdown '{ elementName }' by {selectBy}", true);
                throw;
            }
        }

        protected void SwitchTo_Window(Window window, string windowName)
        {
            try
            {
                var windowHandles = WaitForNewWindow();
                DriverManager.GetDriver().SwitchTo().Window(windowHandles[Convert.ToInt32(window)]);
                ExtentLogger.Pass($"Switched to window '{ windowName }'", true);
            }
            catch (Exception)
            {
                ExtentLogger.Fail($"Failed switching to '{ windowName }' window. Please check!", true);
                throw;
            }
        }

        protected void SwitchTo_iFrame(By element, string frameName)
        {
            try
            {
                new WebDriverWait(DriverManager.GetDriver(), TimeSpan.FromSeconds(10))
                    .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt(element));
                ExtentLogger.Pass($"Switched to iFrame: '{ frameName }'", true);
            }
            catch (Exception)
            {
                ExtentLogger.Fail($"Failed switching to an iFrame: '{ frameName }'. Please check!", true);
                throw;
            }
        }
    }
}
