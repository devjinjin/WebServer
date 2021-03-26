using System.Collections.Generic;
using System.Threading.Tasks;
using WebServer.Models.Features;

namespace WebServer.Data.Common
{
    public interface ICrudRepository<T>
    {
        Task<T> AddAsync(T model);// 입력

        Task<List<T>> GetAllAsync(); // 출력

        Task<PagedList<T>> GetAllAsync(PageParameters parameters);


        //Task<NoteModel> GetByIdAsync(int id); // 상세
        //Task<bool> EditAsync(NoteRequest model); // 수정
        //Task<bool> DeleteAsync(int id); // 삭제
        //Task<PagingResult<NoteModel>> GetAllAsync(int pageIndex, int pageSize); // 페이징
        //Task<PagingResult<NoteModel>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId); // 부모
        //Task<PagingResult<NoteModel>> SearchAllAsync(int pageIndex, int pageSize, string searchQuery); // 검색
        //Task<PagingResult<NoteModel>> SearchAllByParentIdAsync(int pageIndex, int pageSize, string searchQuery, int parentId); // 검색+부모


        //Task<Tuple<int, int>> GetStatus(int parentId);
        //Task<bool> DeleteAllByParentId(int parentId);
        //Task<SortedList<int, double>> GetMonthlyCreateCountAsync();
    }
}
