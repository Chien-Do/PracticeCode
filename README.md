
+Coffee Type


+Coffee Preferences
 -code smell:
 + AustralianCoffeeSearch variable fix for Australia only
 
 -suggestion:
 + Should store 1 count variable
 + Should have 1 more field for Country
 + Every time search update by country and increase count

+DataAcessLayer 
 1. GetAmericanCoffeePrice: 
	americanCoffees could be empty => throw exception when get first item
 2. SearchAustralianCoffeeTypes
    +GetAllCofeeTypes already execute before filter=> Performance not good: _context.CoffeeTypes.Where().Tolist();
	+var cp = _context.CoffeePreferences.SingleOrDefault(); 
	- cp not meaning: change to coffeeprefs or coffeepreferences
	- this line will throw exeption if multiple items match
	- we should use this to fetching data by Id
	- suggestion: use FirstOrDefault() which condition by Country, then check null
	+ purpose: This one is hardcoded for Australia only, we can make it generic by accept country code as parameters
	+ Performance issue : it will cause performance issue because every time search, we update data. We open 2 connection
	- suggestion: 1. use async/await to make it asynchronous to avoid blocking main thread.  This is particularly useful when the code runs in an asynchronous task or in a web application to avoid freezing the web page during saving.
				  2. Use AsNoTracking()
				  3. for better Performance : every store in cache, run background job in period time to update count

 3. IncreasePrices
    + Move SaveChanges outside loop to reduce number of database roundtrip and improve performance 
	+ Use FirstOrDefault instead of Single: Single will throw exception if more than one matching
