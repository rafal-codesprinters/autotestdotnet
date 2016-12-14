using System;
using OpenQA.Selenium;

namespace SeleniumTests
{
    internal class WebDriverWait
    {
        private IWebDriver driver;
        private TimeSpan timeSpan;

        public WebDriverWait(IWebDriver driver, TimeSpan timeSpan)
        {
            this.driver = driver;
            this.timeSpan = timeSpan;
        }

        internal void Until(Func<IWebDriver, IWebElement> func)
        {
            throw new NotImplementedException();
        }
    }
}