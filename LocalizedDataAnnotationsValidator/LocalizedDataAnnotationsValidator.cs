using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

namespace Toolbelt.Blazor.Forms
{
    public class LocalizedDataAnnotationsValidator : ComponentBase
    {
        [Inject]
        private IStringLocalizerFactory StringLocalizerFactory { get; set; }

        [Parameter]
        public string Locale { get; set; } = "";

        [CascadingParameter]
        private EditContext CurrentEditContext { get; set; }

        protected override void OnInitialized()
        {
            CurrentEditContext.AddLocalizedDataAnnotationsValidation(StringLocalizerFactory, Locale);
        }
    }
}
