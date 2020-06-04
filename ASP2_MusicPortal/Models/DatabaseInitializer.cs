using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ASP2_MusicPortal.Models.Entities
{
    public class MusicDatabaseInitializer:CreateDatabaseIfNotExists<MusicPortalModel>
    {
        protected override void Seed(MusicPortalModel context)
        {
            Cities city1 = new Cities { CityName = "Одесса" };
            Cities city2 = new Cities { CityName = "Киев" };
            Cities city3 = new Cities { CityName = "Измаил" };
            Cities city4 = new Cities { CityName = "Николаев" };
            Cities city5 = new Cities { CityName = "Днепр" };
            Cities city6 = new Cities { CityName = "Львов" };
          //  Genres genre1 = new Genres { Genre = "Rock" };
           // Genres genre2 = new Genres { Genre = "Classic" };
            Users users1 = new Users { Name="Administrator", Surname="MusicPortal", Login = "admin", Password = "DbIAsRt9hMw1sYY+d5z1KCbM8WW5LdZdiq09mk8R3bsxmuGH0Qz8j/OAssl/L7+R", Salt = "DeSkJ4CUxt3EXRROFyT122iTmpB3pMk0fbtsT6uLIfc=", isActive = true, isAdmin = true, Cities = city1 };
            Users users2 = new Users { Name = "Svetlana", Surname = "Nekrasova", Login = "sveta", Password = "lesU4AApYp344BX1eCFRvNzyB/hSEl4ce58dDZJdMHVXRfhFEFMKuMNisapb7Kif", Salt = "JFRDDd+iDluRitHQ54aIg4PoprFnsOX6hx0dxQJZtW8=", isActive = false, isAdmin = false, Cities = city6 };

            context.Cities.AddRange(new List<Cities> { city1, city2, city3, city4, city5, city6 });
           // context.Genres.Add(genre1);
           // context.Genres.Add(genre2);
            context.SaveChanges();
            context.Users.AddRange(new List<Users> { users1, users2 });
            context.SaveChanges();
            base.Seed(context);
        }

    }
}