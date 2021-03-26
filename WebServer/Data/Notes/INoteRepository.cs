using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Data.Common;
using WebServer.Models.Features;
using WebServer.Models.Notes;

namespace WebServer.Data.Notes
{
    public interface INoteRepository
    {
        Task AddAsync(NoteRequest model);// 입력

        Task<List<NoteModel>> GetAllAsync(); // 출력
        Task<PagedList<NoteModel>> GetAllAsync(NoteParameters parameters);

        Task<NoteModel> GetByIdAsync(int id); // 상세
        Task<bool> EditAsync(NoteRequest model); // 수정
        Task<bool> DeleteAsync(int id); // 삭제
        Task<PagingResult<NoteModel>> GetAllAsync(int pageIndex, int pageSize); // 페이징
        Task<PagingResult<NoteModel>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId); // 부모
        Task<PagingResult<NoteModel>> SearchAllAsync(int pageIndex, int pageSize, string searchQuery); // 검색
        Task<PagingResult<NoteModel>> SearchAllByParentIdAsync(int pageIndex, int pageSize, string searchQuery, int parentId); // 검색+부모


        Task<Tuple<int, int>> GetStatus(int parentId);
        Task<bool> DeleteAllByParentId(int parentId);
        Task<SortedList<int, double>> GetMonthlyCreateCountAsync();
    }

}
