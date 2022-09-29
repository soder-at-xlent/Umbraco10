using Umbraco.Cms.Core.Strings;

namespace Core.ViewModels
{
    public class StartPageViewModel
    {
        public string Title { get; set; }
        public IHtmlEncodedString MainBody { get; set; }
        public string FooterText { get; set; }
    }
}
