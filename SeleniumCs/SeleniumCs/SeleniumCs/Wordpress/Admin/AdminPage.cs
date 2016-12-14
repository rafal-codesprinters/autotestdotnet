using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumCs
{
    public static class AdminPageExtensions
    {
        public static AdminPageWrapper AdminPage(this IWebDriver driver)
        {
            return new AdminPageWrapper(driver);
        }
    }

    public class AdminPageWrapper
    {
        private IWebDriver driver;

        public AdminPageWrapper(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void LogOut()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.XPath(@"//*[@id=""wp-admin-bar-my-account""]/a")));
            driver.FindElement(By.XPath(@"//*[@id=""wp-admin-bar-my-account""]/a")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable((By.CssSelector("button.ab-sign-out"))));
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
        }

        public void CreateNewPost()
        {
            driver.FindElement(By.XPath(@"//li[@id='menu-posts']/a")).Click();
            driver.FindElement(By.LinkText("Add New")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.Id("publish")));
        }
    }
}
