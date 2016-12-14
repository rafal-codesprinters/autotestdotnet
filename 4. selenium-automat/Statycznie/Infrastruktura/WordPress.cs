namespace Automat.Statycznie.Infrastruktura
{
    internal static class WordPress
    {
        internal static void Wyloguj()
        {
            Test.Driver.Navigate().GoToUrl(WordpressConfiguration.BaseUrl + "/wp-login.php?action=logout");
        }
    }
}