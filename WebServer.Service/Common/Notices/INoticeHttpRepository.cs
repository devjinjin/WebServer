using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Notices;

namespace WebServer.Service.Common.Notices
{
    public interface INoticeHttpRepository : IHttpRepository<NoticeModel>
    {
         Task<PagingResponse<NoticeModel>> GetItems(NoticeParameters noteParameters);

    }
}
