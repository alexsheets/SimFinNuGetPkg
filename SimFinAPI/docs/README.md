SimFin NuGet Package to Easily Perform C# Requests

#--------------------------------------------------------

NOTICE: must supply SimFin API key to instantiate the class/package, then use that instance to complete any requests necessary with provided API key.
All requests return use the verbose version of the requests which return Json which can then be deserialized and queried.

Current built-out requests and parameters required:

--> General Company Data: ticker (string)

--> Financial Statements: ticker (string), statement type (string), fiscal year (string), and period (string).
Statement types include: bs (balance sheet), pl (profit & loss), cf (cash flow), and derived (derived ratios and indicators)