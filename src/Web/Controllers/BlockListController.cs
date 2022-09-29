using Core.Models;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Web.Controllers
{
    public class BlockListController : RenderControllerBase
    {
        private readonly ILogger<RenderController> _logger;
        private readonly IPublishedValueFallback _publishedValueFallback;

        public BlockListController(
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
            var landingPage = new BlockList(CurrentPage, _publishedValueFallback);

            var viewModel = new BlockListViewModel { BlockLists = landingPage.BlockLists };

            var model = await LayoutViewModel<BlockListViewModel>.CreateAsync(viewModel, CurrentPage);

            return View($"~/Views/{nameof(BlockList)}.cshtml", model);
        }
    }
}
