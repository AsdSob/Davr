using System.Collections.Generic;
using System.Linq;

namespace Davr.Gumon.Services
{

    public interface IPageResponseService
    {
        PageResponseModel<TDto> GetPageResponse<TDto>(IEnumerable<TDto> dtos, int totalItems, PageRequestModel pageRequest);
        PageResponseModel<TDto> GetPageResponse<TDto>(IEnumerable<TDto> dtos, int totalItems);
        PageResponseModel<TDto> GetPageResponse<TDto>(PageRequestModel pageRequest);
    }

    public class PageResponseService : IPageResponseService
    {
        public PageResponseModel<TDto> GetPageResponse<TDto>(PageRequestModel pageRequest)
        {
            var pageResponse = new PageResponseModel<TDto>()
            {
                Page = pageRequest.page == 0 ? 1 : pageRequest.page,
                PageSize = pageRequest.pagesize == 0 ? 20 : pageRequest.pagesize,
            };

            return pageResponse;
        }

        public PageResponseModel<TDto> GetPageResponse<TDto>(IEnumerable<TDto> entities, int totalItems)
        {
            var pageResponse = new PageResponseModel<TDto>()
            {
                Page = 1,
                PageSize = 20,
                Items = entities.ToArray(),
                Total = totalItems,
            };

            return pageResponse;
        }

        public PageResponseModel<TDto> GetPageResponse<TDto>(IEnumerable<TDto> items, int totalItems, PageRequestModel pageRequest)
        {
           var pageResponse = new PageResponseModel<TDto>()
            {
                Page = pageRequest.page == 0 ? 1 : pageRequest.page,
                PageSize = pageRequest.pagesize == 0 ? 20 : pageRequest.pagesize,
                Items = items.ToArray(),
                Total = totalItems,
            };

            return pageResponse;
        }

    }
}
