using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace JJServicios.DB.Interface
{
    public interface IApplicationContext : IDisposable
    {
        bool Exists { get; }
        string GetTableName<T>() where T : class;
        IDbSet<T> Set<T>() where T : class;
        DbEntityEntry Entry(object entity);
        int SaveChanges();
        IEnumerable<T> SqlQuery<T>(string query, params object[] parameters);
        int SqlCommand(string query, object[] parameters);

        void BeginTransaction();

        void CommitTransaction();
    }
}
