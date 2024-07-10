# ToDo Web API -- TodoWithSerilogAndCustomMiddlewareLogging
This repository contains the ToDo Web API, built using ASP.NET Core, Entity Framework, Microsoft SQL Server and Swagger.

The API allow user to show all available ToDo, show a single ToDo, Create, Update and Delete a Todo. The API can be accessed using Swagger UI.

## Technologies Used
- .NET Core 8.0
- Entity Framework
- Microsoft SQL SERVER
- Swagger

## Getting Started

To get started with this project, follow these steps:

## Database Connection
To connect to your Microsoft SQL Server database, you need to update the connection string in the appsettings.json file with your own database information.

Here is an example of the connection string format:

```
Server=<server_name>;Database=<database_name>;Trusted_Connection=True;TrustServerCertificate=True
"Server=(localdb)\\MSSQLLocalDB;Database=ToDoDb;Trusted_Connection=True;TrustServerCertificate=True"
```

### Installing

1. Clone the repository to your local machine.

```
https://github.com/mittalv3/ToDoAPIs.git
```
2. Go to the Todo.API directory
3. Open `Todo.sln` file using Visual Studio 2022
4. Open Package Manager Console
5. Run `Update-Package -Verbose` to install required dependencies.
6. Fix the database connection and run `Update-Database -Verbose`  (No need for MIgration command to create local DB, uses Database.EnsureCreated())
7. Select the Todo.API project and run it.
   

## Authentication
For this exercise project, the user will always receive the valid JWt token (through Authentication API within Swagger file) to access the ToDo API, no matter what username and password he provides.

## Usage
The following endpoints are available:

![ToDo Swagger](https://private-user-images.githubusercontent.com/44603730/322143663-c0e9d5f8-cf6a-4998-9074-6fa0d934d167.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3MTI5NTc1MTksIm5iZiI6MTcxMjk1NzIxOSwicGF0aCI6Ii80NDYwMzczMC8zMjIxNDM2NjMtYzBlOWQ1ZjgtY2Y2YS00OTk4LTkwNzQtNmZhMGQ5MzRkMTY3LnBuZz9YLUFtei1BbGdvcml0aG09QVdTNC1ITUFDLVNIQTI1NiZYLUFtei1DcmVkZW50aWFsPUFLSUFWQ09EWUxTQTUzUFFLNFpBJTJGMjAyNDA0MTIlMkZ1cy1lYXN0LTElMkZzMyUyRmF3czRfcmVxdWVzdCZYLUFtei1EYXRlPTIwMjQwNDEyVDIxMjY1OVomWC1BbXotRXhwaXJlcz0zMDAmWC1BbXotU2lnbmF0dXJlPTkzZjMxZmYyMjNiMzNiYjhmYzczZDYyNjM0Y2RiNTdmNzA5MTkxNzBiMWJiZDYzZWZhZmJkNDUzMTZiNzQxMmMmWC1BbXotU2lnbmVkSGVhZGVycz1ob3N0JmFjdG9yX2lkPTAma2V5X2lkPTAmcmVwb19pZD0wIn0.3zrs314G1YqacVQL4GmCysanCszw4JAgmxyxIN8ER70)


## Serilog-Serilog
Connected with local splunk