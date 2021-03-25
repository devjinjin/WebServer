using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Models.Notes
{
    public class NoteModel
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 상단 고정: 공지글로 올리기
        /// </summary>
        public bool? IsPinned { get; set; } = false;

        public string Title { get; set; }

        public string Content { get; set; }

        public string FilePath { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

    }
}
