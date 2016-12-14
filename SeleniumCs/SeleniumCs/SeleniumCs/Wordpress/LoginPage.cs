using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumCs
{
    public static class LoginPageExtensions
    {
        public static LoginPageWrapper LoginPage(this IWebDriver driver)
        {

            return new LoginPageWrapper(driver);
        }

     
    }

    public class LoginPageWrapper
    {
        private IWebDriver driver;

        public LoginPageWrapper(IWebDriver driver)
        {
            this.driver = driver;
        }

        private const string LOGIN_USER_LOGIN_ID = "user_login";
        private const string LOGIN_USER_PASS_ID = "user_pass";
        private const string LOGIN_SUBMIT_BUTTON_ID = "wp-submit";

        public void Navigate(string baseURL)
        {
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php?redirect_to=https%3A%2F%2Fautotestdotnet.wordpress.com%2Fwp-admin%2F&reauth=1");
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable((By.Id(LOGIN_SUBMIT_BUTTON_ID))));
        }

        public void NavigateAndLogIn(string baseURL, string login, string password)
        {
            Navigate(baseURL);
            LogIn(login, password);
        }

        public void LogIn(string login, string password)
        {
            driver.FindElement(By.Id(LOGIN_USER_LOGIN_ID)).Click();
            driver.FindElement(By.Id(LOGIN_USER_LOGIN_ID)).Clear();
            driver.FindElement(By.Id(LOGIN_USER_LOGIN_ID)).SendKeys(login);
            driver.FindElement(By.Id(LOGIN_USER_PASS_ID)).Click();
            driver.FindElement(By.Id(LOGIN_USER_PASS_ID)).Clear();
            driver.FindElement(By.Id(LOGIN_USER_PASS_ID)).SendKeys(password);
            driver.FindElement(By.Id(LOGIN_SUBMIT_BUTTON_ID)).Click();
        }
    }
}
