SimFin NuGet Package to Easily Perform C# Requests

#--------------------------------------------------------

NOTICE: must supply SimFin API key to instantiate the class/package, then use that instance to complete any requests necessary with provided API key.

Current built-out requests and parameters required:

--> General Company Data: ticker (string)

--> Retrieve Filings: ticker (string)

--> Financial Statements (Compact or Verbose): ticker (string), statement type (string), fiscal year (string), and period (string).
Statement types include: bs (balance sheet), pl (profit & loss), cf (cash flow), and derived (derived ratios and indicators)

--> Common Shares Outstanding: ticker (string), start date (YYYY-MM-DD), end date (YYYY-MM-DD)

--> Weighted Shares Outstanding: ticker (string), start date (YYYY-MM-DD), end date (YYYY-MM-DD)

--> Price (stock) data (Compact or Verbose): ticker (string), fiscal year (string), start date (YYYY-MM-DD), end date (YYYY-MM-DD)
