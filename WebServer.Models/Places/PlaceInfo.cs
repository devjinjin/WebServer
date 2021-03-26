using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Models.Places
{
    public class PlaceInfo
    {

        /// <summary>
        /// 아이디
        /// </summary>
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int PlaceId { get; set; }
        #region 장소 설명
        /// <summary>
        /// 제목
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 서브 타이틀 설명등
        /// </summary>
        public string Discription { get; set; }

        /// <summary>
        /// 내용
        /// </summary>
        public string Content { get; set; }
        #endregion

        #region 장소 정보

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        /// <summary>
        /// 주소
        /// </summary>
        public string Address { get; set; }
        

        /// <summary>
        /// 전화번호
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 홈페이지
        /// </summary>
        public string HomePage { get; set; }

        /// <summary>
        /// 등록일
        /// </summary>
        public DateTime RegistDate { get; set; }

        public string MainImage { get; set; }
        #endregion

        /// <summary>
        /// 즐겨찾기 숫자
        /// </summary>
        public int KeepCount { get; set; }

    }
}
