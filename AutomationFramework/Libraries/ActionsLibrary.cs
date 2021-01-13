using static AutomationFramework.Libraries.EnumLibrary;
using static AutomationFramework.Libraries.UtilityLibrary;
using AutomationFramework.Configuration.ReportConfig;
using OpenQA.Selenium;
using System;


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
                ExtentLogger.Pass($"Getting text from { elementName }", true);
            }
            catch (Exception)
            {
                ExtentLogger.Fail($"Failed to get text from { elementName }", true);
                throw;
            }
            return text;
        }
    }
}
