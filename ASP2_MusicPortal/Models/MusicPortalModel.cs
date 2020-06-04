namespace ASP2_MusicPortal.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MusicPortalModel : DbContext
    {
        // Your context has been configured to use a 'MusicPortalModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ASP2_MusicPortal.Models.MusicPortalModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MusicPortalModel' 
        // connection string in the application configuration file.
        public MusicPortalModel()
            : base("name=MusicPortalLocalDB")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Phonoteka> Phonotekas { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}