namespace BunnySurfers.Blazor.Services
{
    public interface IReturnUrlService
    {
        string GetReturnUrl();
        void SetReturnUrl(string returnUrl);
    }

    public class ReturnUrlService : IReturnUrlService
    {
        private string _returnUrl = "/StudentPage"; // Default ReturnUrl

        public string GetReturnUrl()
        {
            return _returnUrl;
        }

        public void SetReturnUrl(string returnUrl)
        {
            _returnUrl = returnUrl;
        }
    }
}
