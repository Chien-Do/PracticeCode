using System.Collections.Generic;
using System.Data.Entity;

namespace CoffeeShopSample.Logic
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(string connectionString) : base(connectionString)
        {
        }

        public IDbSet<CoffeeType> CoffeeTypes { get; }
        public IDbSet<CoffeePreference> CoffeePreferences { get; }

        public IDbSet<CoffeePreferenceNew> CoffeePreferenceNews { get; }

    }
}
