namespace CoffeeShopSample.Logic
{
    public class CoffeePreference
    {
        public int AustralianCoffeeSearch { get; set; }
    }

    public class CoffeePreferenceNew
    {
        public int SearchCount { get; set; }
        public string Country { get; set; }
        public CoffeePreferenceNew()
        {
            SearchCount = 1;
        }

    }
}
