
namespace Davr.Vash.Services
{
    public class PageResponseModel<TDto>
    {
        /// <summary>
        /// Current page
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total finded items
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Items of current page
        /// </summary>
        public TDto[] Items { get; set; }

    }
}
