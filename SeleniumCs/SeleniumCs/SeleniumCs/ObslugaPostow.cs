using System;
using System.Text;
using OpenQA.Selenium;
using Xunit;
using OpenQA.Selenium.Chrome;
using SeleniumCs;

namespace SeleniumTests
{
    public class ObslugaPostow : IDisposable
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;
        private WordpressConfiguration m_config;

        public ObslugaPostow()
        {
            m_config = new WordpressConfiguration();
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            verificationErrors = new StringBuilder();
        }
        
        public void Dispose()
        {
            try
            {
                driver.Quit();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.Equal("", verificationErrors.ToString());
        }
        
        [Fact]
        public void CreatingPost()
        {
            driver.LoginPage().NavigateAndLogIn(m_config.BaseUrl, m_config.Login, m_config.Password);
            driver.AdminPage().CreateNewPost();
            var linkToPost = driver.EditPostPage().EditPostPublishPost("test pg", "treœæ: dodawanie");
            driver.AdminPage().LogOut();
            driver.Navigate().GoToUrl(m_config.BaseUrl + linkToPost);
            Assert.NotEqual("Page not found | Site Title", driver.Title);
        }

        [Fact]
        public void CreatingPostAndMovingItToTrash()
        {
            driver.LoginPage().NavigateAndLogIn(m_config.BaseUrl, m_config.Login, m_config.Password);
            driver.AdminPage().CreateNewPost();
            var linkToPost = driver.EditPostPage().EditPostPublishPost("test pg", "treœæ: dodawanie usuwanie");
            var editUri = driver.Url;
            driver.AdminPage().LogOut();
            driver.Navigate().GoToUrl(m_config.BaseUrl + linkToPost);
            Assert.NotEqual("Page not found | Site Title", driver.Title);

            driver.LoginPage().NavigateAndLogIn(m_config.BaseUrl, m_config.Login, m_config.Password);
            driver.Navigate().GoToUrl(editUri);
            driver.EditPostPage().EditPostMovePostToTrash();
            driver.Navigate().GoToUrl(editUri);
            var element = driver.FindElement(By.Id("error-page")).Text;
            Assert.Equal("You can’t edit this item because it is in the Trash. Please restore it and try again.", element);
        }

     

        [Fact]
        public void Paging()
        {
            driver.Navigate().GoToUrl(m_config.BaseUrl);
            var beforeUrl = driver.Url;
            driver.FindElement(By.XPath(@"//*[@id=""nav-below""]/div[@class=""nav-previous""]/a")).Click();
            var afterUrl = driver.Url;
            var articles = driver.FindElements(By.XPath(@"//*[@id=""content""]/article"));
            Assert.NotEmpty(articles);
            Assert.NotEqual(beforeUrl, afterUrl);
        }


    }
}
