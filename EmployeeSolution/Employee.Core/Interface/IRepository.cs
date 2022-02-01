using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(long id);
        Task Insert(T entity);
        Task Insert(IEnumerable<T> entities);

        Task Update(T entity);
        Task Update(IEnumerable<T> entities);
        Task Delete(T entity);
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        int ExecuteSqlCommand(string sql, params object[] parameters);
        List<T> ExecuteStoredProc(string sql, params object[] parameters);
    }
}
