using CoffeeShopSample.Logic;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoffeeController : Controller
    {
        private readonly ExampleDataAcessLayer _exampleDataAcessLayer;
        public CoffeeController(ExampleDataAcessLayer exampleDataAcessLayer)
        {
            _exampleDataAcessLayer = exampleDataAcessLayer;
            
        }

        [HttpGet]
        public async Task<IEnumerable<CoffeeType>> GetCoffeeTypesAsync([FromQuery] string country, CancellationToken cancellationToken )
        {
            var coffeeTypes = await _exampleDataAcessLayer.SearchCoffeeTypesByCountryAsync(country, cancellationToken);

            return coffeeTypes;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
