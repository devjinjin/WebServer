using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Models.Popup
{
    public class PopupModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 제목
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 내용
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 텍스트를 노출하는 타입인지 확인
        /// </summary>
        public bool IsText { get; set; }

        /// <summary>
        /// 이미지 사용시 파일이름
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 노출 설정
        /// </summary>
        public bool IsHide { get; set; }

        /// <summary>
        /// 생성일
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// 종료일
        /// </summary>
        public DateTime Ended { get; set; }

        /// <summary>
        /// 출력하는 팝업 위치 : 0 => Center , 1 => Left, 2=> Right
        /// </summary>
        public int Position { get; set; }
    }
}
