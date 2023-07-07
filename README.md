
+Coffee Type


+Coffee Preferences
 -code smell:
 + AustralianCoffeeSearch variable fixed for Australia only. If we have over a hundred country, code will be duplicated.
 
 -suggestion:
 + Have 1 field to Count search
 + Have 1 field to classify country
 + When search, based country being used, will update count respectively.

+DataAcessLayer 
 1. GetVietnameseCoffeeTypes:
    - hardcode value "**Vietnam**": Should accept "**Vietnam**" as parameter, and rename function
 2. GetAmericanCoffeePrice: 
    - americanCoffees[0].Price : Could got error, if the list have no item.
 3. SearchAustralianCoffeeTypes
    - GetAllCofeeTypes:
    	+ already execute before filter=> Performance not good
    - var cp = _context.CoffeePreferences.SingleOrDefault(); 
	- cp not meaning: change to coffeeprefs or coffeepreferences
	- this line will throw exeption if multiple items match
    - Purpose: This function is harcoded to search Australian coffee type only.
    - Performance issue : it will cause performance issue because every time search, we get and update data. We open 2 connection
    =>> suggestion:
        + use **FirstOrDefault()** which condition by Country, then check null
	+ Rename function like SearchCoffeeTypesByCountryAsync which can be used to search for any country instead of Australia only.
	+ Instead of fixed country "Austrlia", accept it as parameters.
        + Use async/await to make it asynchronous to avoid blocking main thread. This is particularly useful when the code runs in an asynchronous task or in a web application to avoid freezing the web page during saving.
        + Use AsNoTracking() when get list coffee to let EF know we will not update the data for these entities.
        + Move code increase search count to another private method, based on country when search, will check whether the preferences exist or not, to create/update respectively.
        + Another approach:
        	1. Everytime we search, increase count by country and saved to Cache.
          	2. Along with that, will configure background job will run by period of time, to get data from cache and save to database.
          	

 3. IncreasePrices
    - Call savechange inside loop.
   =>> suggestion:
    + Move SaveChanges outside loop to reduce number of database roundtrip and improve performance 
	+ Use FirstOrDefault instead of Single: Single will throw exception if more than one matching
