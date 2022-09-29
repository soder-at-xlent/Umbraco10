using Core.Models;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Web.Controllers
{
    public class StartPageController : RenderControllerBase
    {
        private readonly ILogger<RenderController> _logger;
        private readonly IPublishedValueFallback _publishedValueFallback;

        public StartPageController(
            ILogger<RenderController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor,
            IPublishedValueFallback publishedValueFallback) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _logger = logger;
            _publishedValueFallback = publishedValueFallback;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var startPage = new StartPage(CurrentPage, _publishedValueFallback);
            var title = string.IsNullOrWhiteSpace(startPage.Title) ? CurrentPage?.Name : startPage.Title;

            var viewModel = new StartPageViewModel { MainBody = startPage.MainBody, FooterText = startPage.FooterText, Title = title };

            var model = await LayoutViewModel<StartPageViewModel>.CreateAsync(viewModel, CurrentPage);

            return View($"~/Views/{nameof(StartPage)}.cshtml", model);
            //return CurrentTemplate(model);
        }
    }
}
