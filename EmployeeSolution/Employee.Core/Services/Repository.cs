using Employee.Core.Interface;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Employee.Core.Services
{

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _entities;

        public Repository(ApplicationContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            try
            {
                return _entities.AsEnumerable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<T> GetById(long id)
        {
            try
            {
                return await _entities.FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                 _entities.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task Insert(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                await _entities.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async virtual Task Update(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return _entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return _entities.AsNoTracking();
            }
        }

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            try
            {
                var sqlString = new StringBuilder();
                sqlString.Append(sql);
                if (parameters != null && parameters.Length > 0)
                {
                    for (int i = 0; i <= parameters.Length - 1; i++)
                    {
                        var p = parameters[i] as DbParameter;
                        if (p == null)
                            throw new Exception("Not support parameter type");

                        sqlString.Append(i == 0 ? " " : ", ");
                        sqlString.Append("@" + p.ParameterName);
                        if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                        {
                            //output parameter
                            sqlString.Append(" output");

                        }
                    }
                }

                var result = _context.Database.ExecuteSqlCommand(sqlString.ToString(), parameters);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Executes the given DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        public DataTable GetSqlCommandData(string sql)
        {
            try
            {
                var dataTable = new DataTable();
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    _context.Database.OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dataTable.Load(reader);
                        }
                        return dataTable;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> ExecuteStoredProc(string sql, params object[] parameters)
        {
            try
            {
                var sqlString = new StringBuilder();
                sqlString.Append(sql);
                if (parameters != null && parameters.Length > 0)
                {
                    for (int i = 0; i <= parameters.Length - 1; i++)
                    {
                        var p = parameters[i] as DbParameter;
                        if (p == null)
                            throw new Exception("Not support parameter type");

                        sqlString.Append(i == 0 ? " " : ", ");
                        sqlString.Append("@" + p.ParameterName);
                        if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                        {
                            //output parameter
                            sqlString.Append(" output");

                        }
                    }
                }

                var result = _entities.FromSql(sqlString.ToString(), parameters).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}