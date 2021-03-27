using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Data.Common;
using WebServer.Models.Notes;

namespace WebServer.Data.Notes
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        ////public IQueryable<Note> Notes => throw new NotImplementedException();

        public NoteRepository(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            this._logger = loggerFactory.CreateLogger(nameof(NoteRepository));
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }

        /// <summary>
        /// 입력
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public async Task AddAsync(NoteRequest model)
        {
            try
            {

                NoteModel note = new NoteModel()
                {
                    Name = model.Name,
                    Title = model.Title,
                    Content = model.Content,
                    FilePath = model.FilePath,
                    CreatedBy = model.CreatedBy,
                    Created = DateTime.Now
                };
                _context.Notes.Add(note);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR({nameof(AddAsync)}): {e.Message}");
            }

            
        }

        /// <summary>
        /// 출력
        /// </summary>
        /// <returns></returns>

        public async Task<List<NoteModel>> GetAllAsync()
        {
            return await _context.Notes.OrderByDescending(m => m.Id).ToListAsync();
        }

        public async Task<PagedList<NoteModel>> GetAllAsync(NoteParameters parameters)
        {
            var notes = await _context.Notes
                .Search(parameters.SearchTerm)
                .Sort(parameters.OrderBy)
                .ToListAsync();

            return PagedList<NoteModel>
                .ToPagedList(notes, parameters.PageNumber, parameters.PageSize);
        }

        /// <summary>
        /// 상세
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<NoteModel> GetByIdAsync(int id)
        {
            var note = await _context.Notes.SingleOrDefaultAsync(m => m.Id == id);
            note.ReadCnt += 1;
            _context.Entry(note).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return note;
        }


        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync(NoteRequest model)
        {
            try
            {

                NoteModel note = new NoteModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Title = model.Title,
                    Content = model.Content,
                    FilePath = model.FilePath,
                    CreatedBy = model.CreatedBy,
                    Modified = DateTime.Now
                };

                _context.Notes.Attach(note);
                _context.Entry(note).State = EntityState.Modified;

               
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR({nameof(EditAsync)}): {e.Message}");
            }

            return false;
        }

            /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var model = await _context.Notes.FindAsync(id);
                _context.Remove(model);
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR({nameof(DeleteAsync)}): {e.Message}");
            }

            return false;
        }


        // 부모
        public async Task<PagingResult<NoteModel>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId)
        {
            var totalRecords = await _context.Notes.Where(m => m.ParentId == parentId).CountAsync();
            var models = await _context.Notes
                .Where(m => m.ParentId == parentId)
                .OrderByDescending(m => m.Id)
                //.Include(m => m.NoticesComments)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<NoteModel>(models, totalRecords);
        }

        // 상태
        public async Task<Tuple<int, int>> GetStatus(int parentId)
        {
            var totalRecords = await _context.Notes.Where(m => m.ParentId == parentId).CountAsync();
            var pinnedRecords = await _context.Notes.Where(m => m.ParentId == parentId && m.IsPinned == true).CountAsync();

            return new Tuple<int, int>(pinnedRecords, totalRecords); // (2, 10)
        }

        public async Task<bool> DeleteAllByParentId(int parentId)
        {
            try
            {
                var models = await _context.Notes.Where(m => m.ParentId == parentId).ToListAsync();

                foreach (var model in models)
                {
                    _context.Notes.Remove(model);
                }

                return (await _context.SaveChangesAsync() > 0 ? true : false);

            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR({nameof(DeleteAllByParentId)}): {e.Message}");
            }

            return false;
        }

        // 검색
        // 검색
        public async Task<PagingResult<NoteModel>> SearchAllAsync(int pageIndex, int pageSize, string searchQuery)
        {
            var totalRecords = await _context.Notes
                .Where(m => m.Name.Contains(searchQuery) || m.Title.Contains(searchQuery) || m.Title.Contains(searchQuery))
                .CountAsync();
            var models = await _context.Notes
                .Where(m => m.Name.Contains(searchQuery) || m.Title.Contains(searchQuery) || m.Title.Contains(searchQuery))
                .OrderByDescending(m => m.Id)
                //.Include(m => m.NoticesComments)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<NoteModel>(models, totalRecords);
        }

        // 검색+부모
        public async Task<PagingResult<NoteModel>> SearchAllByParentIdAsync(int pageIndex, int pageSize, string searchQuery, int parentId)
        {
            var totalRecords = await _context.Notes.Where(m => m.ParentId == parentId)
                .Where(m => EF.Functions.Like(m.Name, $"%{searchQuery}%") || m.Title.Contains(searchQuery) || m.Title.Contains(searchQuery))
                .CountAsync();
            var models = await _context.Notes.Where(m => m.ParentId == parentId)
                .Where(m => m.Name.Contains(searchQuery) || m.Title.Contains(searchQuery) || m.Title.Contains(searchQuery))
                .OrderByDescending(m => m.Id)
                //.Include(m => m.NoticesComments)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<NoteModel>(models, totalRecords);
        }

        public async Task<SortedList<int, double>> GetMonthlyCreateCountAsync()
        {
            SortedList<int, double> createCounts = new SortedList<int, double>();

            // 1월부터 12월까지 0.0으로 초기화
            for (int i = 1; i <= 12; i++)
            {
                createCounts[i] = 0.0;
            }

            for (int i = 0; i < 12; i++)
            {
                // 현재 달부터 12개월 전까지 반복
                var current = DateTime.Now.AddMonths(-i);
                var cnt = _context.Notes.AsEnumerable().Where(
                    m => m.Created != null
                    &&
                    Convert.ToDateTime(m.Created).Month == current.Month
                    &&
                    Convert.ToDateTime(m.Created).Year == current.Year
                ).ToList().Count();
                createCounts[current.Month] = cnt;
            }

            return await Task.FromResult(createCounts);
        }



        public async Task<PagingResult<NoteModel>> GetAllAsync(int pageIndex, int pageSize)
        {
            var totalRecords = await _context.Notes.CountAsync();
            var models = await _context.Notes
                .OrderByDescending(m => m.Id)
                //.Include(m => m.NoticesComments)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<NoteModel>(models, totalRecords);
        }
    }
}
   
