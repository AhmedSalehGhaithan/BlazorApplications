using Microsoft.AspNetCore.Components;
namespace Component.Pages
{
    public partial class Dismissible
    {
        [Parameter]
        public bool Show { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;
        public void Dismiss() => Show = false;
    }
}