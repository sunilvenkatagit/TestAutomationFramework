using static AutomationFramework.Libraries.UtilityLibrary;
using static AutomationFramework.Libraries.EnumLibrary;
using AutomationFramework.Configuration.DriverConfig;
using AutomationFramework.Configuration.ReportConfig;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;

namespace AutomationFramework.Libraries
{
    public class ActionsLibrary
    {
        protected void ClickOnElement(By element, WaitStrategy waitStrategy, string elementName)
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

        protected void ClickUsingJavaScript(By element, WaitStrategy waitStrategy, string elementName)
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

        protected void EnterText(By element, string text, WaitStrategy waitStrategy, string elementName)
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

        protected string GetText(By element, WaitStrategy waitStrategy, string elementName)
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

        protected string GetValueByAttribute(By element, WaitStrategy waitStrategy, string attribute, string elementName)
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

        protected void SelectDropdown(By element, SelectBy selectBy, string value, string elementName)
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
                ExtentLogger.Pass($"Selected '{ value }' from dropdown { elementName }", true);
            }
            catch (Exception)
            {
                ExtentLogger.Fail($"Failed to select { value } from dropdown '{ elementName }' by {selectBy}", true);
                throw;
            }
        }

        protected void SwitchTo_Window()
        {

        }

        protected void SwitchTo_iFrame()
        {
        }

        protected void ClickUsingActions()
        {
        }
    }
}
