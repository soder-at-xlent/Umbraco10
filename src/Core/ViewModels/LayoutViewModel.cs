using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Core.ViewModels;

public class LayoutViewModel : ContentModel
{
    protected LayoutViewModel(
        IPublishedContent content) : base(content)
    {
        var titleProperty = content.Value<string>("title", fallback: Fallback.ToAncestors);
        Title = !string.IsNullOrWhiteSpace(titleProperty) ? titleProperty : content.Name;
        Subtitle = content.Value<string>("subtitle", fallback: Fallback.ToAncestors);
        MenuItems = content.Root().Children
            .Select(child => new LayoutMenuItem { Name = child.Name, Url = child.Url() }).ToList();
    }

    public string Title { get; set; }
    public string Subtitle { get; set; }
    public IReadOnlyCollection<LayoutMenuItem> MenuItems { get; set; }
}

public class LayoutMenuItem
{
    public string Name { get; set; }
    public string Url { get; set; }
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