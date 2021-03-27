using System;

namespace WebServer.Models.Notices
{
    public class NoticeModel
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int ReadCnt { get; set; } = 0;

        public bool IsPinned { get; set; } = false;

        public DateTime Created { get; set; }
    }
}
