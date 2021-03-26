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

        #region 운영정보
        /// <summary>
        /// 시작시간
        /// </summary>
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// 마감시간
        /// </summary>
        public DateTime CloseTime { get; set; }

        /// <summary>
        /// 휴무일
        /// </summary>
        public string CloseDay { get; set; }
        #endregion

        #region 가격정책
        /// <summary>
        /// 할인 가격
        /// </summary>
        public Double Price { get; set; }

        /// <summary>
        /// 장소 알림
        /// </summary>
        public string PlaceNotice { get; set; }
        #endregion

        #region 장소 정보

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        /// <summary>
        /// 주소
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// 우편번호
        /// </summary>
        public string PostAddress { get; set; }


        /// <summary>
        /// 원래 가격
        /// </summary>
        public Double OriginPrice { get; set; }

        /// <summary>
        /// 회사 아이디
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 회사명
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 담당자
        /// </summary>
        public string Manager { get; set; }
        /// <summary>
        /// 전화번호
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 홈페이지
        /// </summary>
        public string HomePage { get; set; }

        /// <summary>
        /// 이메일
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 등록일
        /// </summary>
        public DateTime RegistDate { get; set; }
        #endregion

        /// <summary>
        /// 즐겨찾기 숫자
        /// </summary>
        public int KeepCount { get; set; }
    }
}
