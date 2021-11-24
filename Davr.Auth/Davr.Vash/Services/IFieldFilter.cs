
using System.ComponentModel.DataAnnotations;

namespace Davr.Vash.Services
{
    public interface IFieldFilter
    {

    }

    public class FieldFilter : IFieldFilter
    {
        [Required(AllowEmptyStrings = false)]
        public string Field { get; set; }

        public string Value { get; set; }
    }
}
