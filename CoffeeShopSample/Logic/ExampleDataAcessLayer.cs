using System.Data.Entity;
using System.Linq.Expressions;

namespace CoffeeShopSample.Logic
{
    public class ExampleDataAcessLayer
    {
        private readonly AppDbContext _context;
        public ExampleDataAcessLayer(AppDbContext appDbContext)
        {
            _context = appDbContext;

        }
        #region Original code
        public IEnumerable<CoffeeType> GetVietnameseCoffeeTypes()
        {
            return _context.CoffeeTypes.Where(ct => ct.Name == "Vietnam");

        }

        public decimal GetAmericanCoffeePrice()
        {
            var americanCoffees = _context.CoffeeTypes.Where(ct => ct.CountryOfOrigin == "America").ToList();

            return americanCoffees[0].Price;
        }

        public IEnumerable<CoffeeType> SearchAustralianCoffeeTypes()
        {
            var types = GetAllCofeeTypes().Where(r => r.CountryOfOrigin == "Australia").ToList();
            var cp = _context.CoffeePreferences.SingleOrDefault();
            cp.AustralianCoffeeSearch++;
            _context.SaveChanges();

            return types;
        }

        private IEnumerable<CoffeeType> GetAllCofeeTypes()
        {
            return _context.CoffeeTypes.ToList();
        }
        public void IncreasePrices(IEnumerable<CoffeeType> types, double percentage)
        {
            foreach (var coffeeType in types)
            {
                var type = _context.CoffeeTypes.Single(r => r.CoffeeTypeId == coffeeType.CoffeeTypeId);
                type.Price *= 1 + (decimal)percentage;
                _context.SaveChanges();

            }
        }
        #endregion

        #region After Optimization
        public IQueryable<CoffeeType> GetCoffeeTypesByCountry(Expression<Func<CoffeeType, bool>> predicate)
        {
            return _context.CoffeeTypes.Where(predicate);
        }

        public async Task<IEnumerable<CoffeeType>> GetVietnameseCoffeeTypesAsync()
        {
            return await GetCoffeeTypesByCountry(r=> r.CountryOfOrigin == "Vietnam").ToListAsync();
        }

        public async Task<decimal?> GetFirstAmericaCoffeePriceAsync(CancellationToken token)
        {
            var firstAmericaCoffee = await GetCoffeeTypesByCountry(r => r.CountryOfOrigin == "America").Select(ct => ct.Price).FirstOrDefaultAsync(token);
            return firstAmericaCoffee;
        }

        public async Task<IEnumerable<CoffeeType>> SearchCoffeeTypesByCountryAsync(string country, CancellationToken token)
        {
            var types = await _context.CoffeeTypes.Where(ct => ct.CountryOfOrigin == country).AsNoTracking().ToListAsync(token);
            await IncreaseSearchCountAsync(country, token);
            return types;
        }

        private async Task IncreaseSearchCountAsync(string country, CancellationToken token)
        {
            var coffeePreference = await _context.CoffeePreferenceNews.FirstOrDefaultAsync(cp => cp.Country == country, token);

            if (coffeePreference != null)
            {
                coffeePreference.SearchCount++;
            }
            else
            {
                _context.CoffeePreferenceNews.Add(new CoffeePreferenceNew { Country = country });
            }

            await _context.SaveChangesAsync(token);
        }

        public async Task IncreasePricesAsync(IEnumerable<CoffeeType> types, double percentage, CancellationToken token)
        {
            foreach (var type in types)
            {
                type.Price *= (decimal)(1 + percentage);
            }

            await _context.SaveChangesAsync(token);
        }
        #endregion
    }
}
