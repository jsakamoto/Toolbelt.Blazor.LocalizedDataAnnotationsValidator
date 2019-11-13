using System.ComponentModel.DataAnnotations;

namespace SampleApp.Models
{
    // https://docs.microsoft.com/ja-jp/aspnet/core/blazor/forms-validation?view=aspnetcore-3.0
    public class Person
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(10, ErrorMessage = "{0} is too long.")]
        public string Name { get; set; }
    }
}
