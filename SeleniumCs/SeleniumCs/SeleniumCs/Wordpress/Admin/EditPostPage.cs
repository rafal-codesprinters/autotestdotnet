using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumCs
{
    public static class EditPostPageExtensions
    {
        public static EditPostPageWrapper EditPostPage(this IWebDriver driver)
        {
            return new EditPostPageWrapper(driver);
        }
    }

    public class EditPostPageWrapper
    {
        private IWebDriver driver;

        public EditPostPageWrapper(IWebDriver driver)
        {
            this.driver = driver;
        }

        private const string POST_EDIT_TITLE_ID = "title";
        private const string POST_EDIT_CONTENT_ID = "content";
        private const string POST_EDIT_PUBLISH_BUTTON_ID = "publish";

        public string EditPostPublishPost(string postTitle, string postContent)
        {
            driver.FindElement(By.Id(POST_EDIT_TITLE_ID)).Click();
            driver.FindElement(By.Id(POST_EDIT_TITLE_ID)).Clear();
            driver.FindElement(By.Id(POST_EDIT_TITLE_ID)).SendKeys(postTitle);
            driver.FindElement(By.Id(POST_EDIT_CONTENT_ID)).Click();
            driver.FindElement(By.Id(POST_EDIT_CONTENT_ID)).Clear();
            driver.FindElement(By.Id(POST_EDIT_CONTENT_ID)).SendKeys(postContent);
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.Id(POST_EDIT_PUBLISH_BUTTON_ID)));
            driver.FindElement(By.Id(POST_EDIT_PUBLISH_BUTTON_ID)).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementExists(By.CssSelector("#message")));
            var linkToPost = driver.FindElement(By.XPath("//span[@id='sample-permalink']/a")).GetAttribute("href");
            return linkToPost;
        }

        public void EditPostMovePostToTrash()
        {
            driver.FindElement(By.XPath(@"//*[@id=""delete-action""]/a")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementExists(By.CssSelector("#message")));
        }
    }
}
