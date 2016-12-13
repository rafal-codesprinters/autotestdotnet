using System;
using System.Text;

        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheUsuwaniePostuTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php?redirect_to=https%3A%2F%2Fautotestdotnet.wordpress.com%2Fwp-admin%2F&reauth=1");
            driver.FindElement(By.Id("wp-submit")).Click();
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            Assert.AreEqual("Posts ‹ Site Title — WordPress", driver.Title);
            driver.FindElement(By.Id("post-search-input")).Clear();
            driver.FindElement(By.Id("post-search-input")).SendKeys("Lubartowski");
            driver.FindElement(By.Id("search-submit")).Click();
            Assert.AreEqual("Posts Add New Search results for “Lubartowski”", driver.FindElement(By.CssSelector("h1")).Text);
            driver.FindElement(By.LinkText("Krzysztof Lubartowski")).Click();
            Assert.AreEqual("Edit Post Add New", driver.FindElement(By.CssSelector("h1")).Text);
            driver.FindElement(By.LinkText("Move to Trash")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
