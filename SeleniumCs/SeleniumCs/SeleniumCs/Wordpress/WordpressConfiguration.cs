namespace SeleniumCs
{
    public class WordpressConfiguration
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string BaseUrl { get; private set; }

        public WordpressConfiguration()
        {
            Login = "autotestdotnet@gmail.com";
            Password = "codesprinters2016";
            BaseUrl = "https://autotestdotnet.wordpress.com/";
        }
    }
}
