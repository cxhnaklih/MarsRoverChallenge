using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MarsRoverChallengeWebApi.Models
{
    public interface IDataStore: IDisposable
    {
        DbSet<PlateauModel> PlateauModels { get; set; }
        DbSet<MarsRoverModel> MarsRoverModels { get; set; }
        int SaveChanges();
        DbEntityEntry Entry(object entity);
        
    }
}
