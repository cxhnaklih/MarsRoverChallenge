using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRoverChallengeWebApi.Models;
using System.Reflection;

namespace MarsRoverChallengeWebApi.Tests
{
    public class MockDataStore :IDataStore
    {
        public MockDataStore()
        {
            this.PlateauModels = new TestPlateauDbSet();
            
        }
        public System.Data.Entity.DbSet<MarsRoverChallengeWebApi.Models.PlateauModel> PlateauModels
        {
            get;
            set;
        }

        public System.Data.Entity.DbSet<MarsRoverChallengeWebApi.Models.MarsRoverModel> MarsRoverModels
        {
            get;
            set;
        }

        public int SaveChanges()
        {
            return 0;
        }

        public System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity)
        {
            System.Data.Entity.Infrastructure.DbEntityEntry x = (System.Data.Entity.Infrastructure.DbEntityEntry) Activator.CreateInstance(typeof(System.Data.Entity.Infrastructure.DbEntityEntry));

            return x;
        }

        public void Dispose()
        {
        }
    }
}
