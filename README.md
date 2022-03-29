# api-back

A ``.Net 6.0`` WebApi. Repositories, Swagger, Mapper, Serilog and fake database.

# How to run
- Download the .Net SDK 6
- On the root directory, run ``docker-compose up -d --build``
- Go to src/Boilerplate.Api folder (``cd src/Boilerplate.Api``) and run ``dotnet run``
- App is accessible athttp://localhost:5000
- Visit http://localhost:5000/api-docs to access the application's swagger

# Authentication
In this project, some routes requires authentication/authorization. For that, you will have to use the ``api/user/authenticate`` route to obtain the JWT.
As default, you have three users, modifiable in DB.json
1. 
	- email: romane.thu@gmail.com
	- password: Password123!
2. 
	- email: example2@gmail.com
	- password: Password123!
3. 
	- email: example3@gmail.com
	- password: Password123!

After that, you can pass JWT via the Authorization header on a http request.

Connected with a JWT, you can modify your password using the ``api/user/update-password`` route.

## Running tests
In the root folder, run ``dotnet test``. This command will try to find all test projects associated with the sln file.
If you are using Visual Studio, you can also acess the Test Menu and open the Test Explorer, where you can see all tests and run all of them or one specifically. 

# This project contains:
- SwaggerUI
- AutoMapper
- Serilog with request logging and easily configurable sinks
- .Net Dependency Injection
- Authentication
- Container support with [docker](src/Boilerplate.Api/dockerfile) and [docker-compose](docker-compose.yml)

# Project Structure
1. Services
	- This folder stores your apis and any project that sends data to your users.
	1. Boilerplate.Api
		- This is the main api project. Here are all the controllers and initialization for the api that will be used.
	2. docker-compose
		- This project exists to allow you to run docker-compose with Visual Studio. It contains a reference to the docker-compose file and will build all the projects dependencies and run it.
2. Application
	-  This folder stores all data transformations between your api and your domain layer. It also contains your business logic.
	1. Auth
		- This folder contains the login Session implementation.
3. Domain
	- This folder contains your business models, enums and common interfaces.
	1. Boilerplate.Domain.Core
		- Contains the base entity for all other domain entities, as well as the interface for the repository implementation.
	2. Boilerplate.Domain
		- Contains business models and enums.
		1. Auth
			- This folder contains the login Session Interface.
4. Infra
	- This folder contains all data access repositories, database in json format
	1. Boilerplate.Infrastructure
		- This project contains the database in json format, and the User repository accessing and modifying the database

# Migrations
1. To run migrations on this project, run the following command on the root folder: 
	- ``dotnet ef migrations add InitialCreate --startup-project .\src\Boilerplate.Api\ --project .\src\Boilerplate.Infrastructure\``

2. This command will set the entrypoint for the migration (the responsible to selecting the dbprovider { sqlserver, mysql, etc } and the connection string) and the selected project will be "Boilerplate.Infrastructure", which is where the dbcontext is.

# About
This project was created with a boilerplate/template, developed by Yan Pitangui under [MIT license](LICENSE).
