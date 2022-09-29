using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Core.ViewModels;

public class LayoutViewModel : ContentModel
{
    protected LayoutViewModel(
        IPublishedContent content) : base(content)
    {
        var titleProperty = content.Value<string>("title");
        Title = !string.IsNullOrWhiteSpace(titleProperty) ? titleProperty : content.Name;
    }

    public string Title { get; set; }
}

public class LayoutViewModel<T> : LayoutViewModel where T : class
{
    protected LayoutViewModel(T viewModel, IPublishedContent content) : base(content)
    {
        ViewModel = viewModel;
    }

    public T ViewModel { get; set; }

    public static async Task<LayoutViewModel<T>> CreateAsync(T viewModel, IPublishedContent content)
    {
        var model = new LayoutViewModel<T>(viewModel, content);

        return model;
    }
}