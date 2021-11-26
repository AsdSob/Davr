
namespace Davr.Gumon.Services
{
    public class PageRequestModel
    {
        /// <summary>
        /// Current page
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int pagesize { get; set; }

        /// <summary>
        /// Sorting by field
        /// </summary>
        public string sort { get; set; }
    }

    public class PageRequestFilter : PageRequestModel, IFieldFilter
    {
        /// <summary>
        /// Filters
        /// </summary> 
        public FieldFilter[] filters { get; set; }
    }
}
