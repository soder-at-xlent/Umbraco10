using Core.Models;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Web.Controllers
{
    public class LandingPageGridController : RenderControllerBase
    {
        private readonly ILogger<RenderController> _logger;
        private readonly IPublishedValueFallback _publishedValueFallback;

        public LandingPageGridController(
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
            var landingPage = new LandingPageGrid(CurrentPage, _publishedValueFallback);
            var title = CurrentPage?.Name;

            //var viewModel = new StartPageViewModel { MainBody = landingPage.MainBody, FooterText = landingPage.FooterText, Title = title };

            var model = await LayoutViewModel<LandingPageGrid>.CreateAsync(landingPage, CurrentPage);

            return View($"~/Views/{nameof(LandingPageGrid)}.cshtml", model);
        }
    }
}
