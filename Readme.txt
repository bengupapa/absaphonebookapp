NOTES
-----

Solution dir	: absaphonebookapp
Clinet dir		: absaphonebookapp/phonebookclient
Server dir		: absaphonebookapp/phonebookserver

---------------------------
Assumption and scope limits
---------------------------
- This solution does not cater for country specialised phone number formats.
- This solution does not cater for country codes.
- This solution assumes that phone number contains only numbers.
- This solution assimes that phone number has a length of 10 digits.

---------
DB Server
---------
This solution requires Microsoft SQL Server
This solution reserves the database name BNGPAPPhoneBook. Please ensure you don't already have a db with this name.


----------
Client App
----------
Based on angular cli 11.2.0
Reguirements:
- node
- npm

From Client dir, open any shell (recommends Powershell)
- run command: npm install
- run command: ng serve

The client app should be running on the default angular port: http://localhost:4200/


----------
Server App
----------
Based on .net5 SDk and runtime version
Requirements:
- dotnet v5 cli
- dotnet v5 runtime

From Server dir, open any shell (recommends Powershell)
- run dotnet restore
- run dotnet build
- run dotnet run --no-build

The server api should be running on the default kestrel ports: http://localhost:5000/ and httpd://localhost:5001/ (Note: http redirects to https)
