using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Data.Common;
using WebServer.Models.Notes;

namespace WebServer.Data.Notes
{
    public interface INoteRepository
    {

        Task<Note> AddAsync(Note model); // 입력
        Task<List<Note>> GetAllAsync(); // 출력
        Task<Note> GetByIdAsync(int id); // 상세
        Task<bool> EditAsync(Note model); // 수정
        Task<bool> DeleteAsync(int id); // 삭제
        Task<PagingResult<Note>> GetAllAsync(int pageIndex, int pageSize); // 페이징
        Task<PagingResult<Note>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId); // 부모
        Task<PagingResult<Note>> SearchAllAsync(int pageIndex, int pageSize, string searchQuery); // 검색
        Task<PagingResult<Note>> SearchAllByParentIdAsync(int pageIndex, int pageSize, string searchQuery, int parentId); // 검색+부모


        Task<Tuple<int, int>> GetStatus(int parentId);
        Task<bool> DeleteAllByParentId(int parentId);
        Task<SortedList<int, double>> GetMonthlyCreateCountAsync();
    }

}
