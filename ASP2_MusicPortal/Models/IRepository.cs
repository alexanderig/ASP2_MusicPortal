using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP2_MusicPortal.Models.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ASP2_MusicPortal.Models
{
    interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetListofEntities();
        T GetEntity(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);

        void Save();
    }

    public interface IMPRepository
    {
        DbSet<Users> User { get; set; }
        DbSet<Cities> City { get; set; }
        DbSet<Genres> Genre { get; set; }
        DbSet<Phonoteka> Phonotrack { get; set; }
        Task<int> SaveChangesAsync();

    }
}