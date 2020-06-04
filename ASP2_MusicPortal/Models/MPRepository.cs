using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP2_MusicPortal.Models.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ASP2_MusicPortal.Models
{
    public class MPRepository:IMPRepository
    {
        private readonly MusicPortalModel db;
        public MPRepository(MusicPortalModel context)
        {
            db = context;// new MusicPortalModel();
        }

        
        private bool disposed = false;

        public DbSet<Users> User { get { return db.Users; } set { db.Users = value; } }
        public DbSet<Cities> City { get { return db.Cities; } set { db.Cities = value; } }
        public DbSet<Genres> Genre { get { return db.Genres; } set { db.Genres = value; } }
        public DbSet<Phonoteka> Phonotrack { get { return db.Phonotekas; } set { db.Phonotekas = value; } }

        //public virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        public Task<int> SaveChangesAsync()
        {
            return db.SaveChangesAsync();
        }
    }
}