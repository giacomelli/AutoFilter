#AutoFilter

An easy way to auto build expression trees filters from an user input.

--------


##Setup

####NuGet
PM> Install-Package AutoFilter


##Strategies
- BooleanEqualAutoFilterStrategy
- DateTimeEqualAutoFilterStrategy
- DoubleEqualAutoFilterStrategy
- EnumEqualAutoFilterStrategy
- Int32EqualAutoFilterStrategy
- StringContainsAutoFilterStrategy
- StringContainsIgnoreCaseAutoFilterStrategy 

--------

##Usage
```csharp
// The user input string filter.
string filter;

// The data sample.
var data = new SampleTarget[] {
    new SampleTarget() { String = "String ABC", DateTime = new DateTime(2001, 1, 2), Int32 = 1, Boolean = true },
    new SampleTarget() { String = "String CDE", DateTime = new DateTime(2003, 4, 5), Int32 = 2, Boolean = false },
    new SampleTarget() { String = "String EFG", DateTime = new DateTime(2006, 7, 8), Int32 = 3, Boolean = true },
};

// The auto filter builder and strategies selected.
var builder = new AutoFilterBuilder<SampleTarget>(
    new BooleanEqualAutoFilterStrategy(),
    new StringContainsIgnoreCaseAutoFilterStrategy(),
    new DateTimeEqualAutoFilterStrategy(),
    new Int32EqualAutoFilterStrategy());

// Build the auto filter expression using the filter typed by the user.
var expression = builder.Build(filter);

// Filters the data.
var filteredData = data.Where(expression.Compile());
```
In the sample above, the filter variable is a string variable input by the user.
Below the results in filteredData variable for each sample user input.

```csharp
filter = "c";
String: String ABC, DateTime: 02/01/2001 00:00:00, Int32: 1, Boolean: True
String: String CDE, DateTime: 05/04/2003 00:00:00, Int32: 2, Boolean: False

filter = "d";
String: String CDE, DateTime: 05/04/2003 00:00:00, Int32: 2, Boolean: False

filter = "5/4/2003";
String: String CDE, DateTime: 05/04/2003 00:00:00, Int32: 2, Boolean: False

filter = "3";
String: String EFG, DateTime: 08/07/2006 00:00:00, Int32: 3, Boolean: True

filter = "true";
String: String ABC, DateTime: 02/01/2001 00:00:00, Int32: 1, Boolean: True
String: String EFG, DateTime: 08/07/2006 00:00:00, Int32: 3, Boolean: True
```

##EF ready
In other words, all IAutoFilterStrategy implementations can be translated by Entity Framework to SQL where clauses.

##FAQ

####Having troubles? 
 - Ask on [Stack Overflow](http://stackoverflow.com/search?q=AutoFilter)

##Roadmap

  - Add new strategies.
 
--------

##How to improve it?
- Create a fork of [AutoFilter](https://github.com/giacomelli/AutoFilter/fork). 
- Did you change it? [Submit a pull request](https://github.com/giacomelli/AutoFilter/pull/new/master).


##License

Licensed under the The MIT License (MIT).
In others words, you can use this library for developement any kind of software: open source, commercial, proprietary and alien.


##Change Log
 - 0.1.0 First version.
