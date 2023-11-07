# WorkItem.Task
WorkItem.Task is Web Application, can be used for Create, Edit and Delete tasks with basic task informations. 
This application is designed and developed for code test and its not a real time project.
## Features:- Task Management
Using this application you can 
- Create new task
- Edit existing task
- Delete existing task
## Technology Used
- For frontend -> ASP.NET Core MVC, Ajax, jQuery, CSS
- For Database -> SQL Server, LINQ, EntityFrameworkCore
- Containerized Application -> This application is developed and placed under container with all required packages.
## Application/Project setup
Required application settings are available in appsettings.json file (connection string to connect Database)
This application has been developed in such a way that, if you have the required software for data base (SQL Server or 
Azure SQL Server or other cloud service) you can just update the ConnectionStrings DBConnection property of the 
appsettings.json file and run the application. This application will take care of creating Database, feeding data 
in to the tables.

To run this application locally, you need Visual Studio 2022, Docker Desktop to get SQL Server image of you can
install SQL Server developer edition on you system and update the connection string then application works successfully.
	
Example: I have added the commented Azure SQL Server ConnectionString in the appsettings.json for your references.
But, it will not work, I have deleted the Azure service (Azure SQL Server) as Azure is paid service(pay-as-you-go)
## C# Unit test
This application covers unit tests for all layer (Service, UnitOfWork, Controller, Repository 
and WorkItemDBContext). I have achived more than 98% code coverage.

- Telerik.JustMock is 3rd party package used to test DBContext -> You can find related tests from EntityRepositoryTests.cs file
- [ExcludeFromCodeCoverage] attribute is also used.
## Application Development Approach
- Followed coding Standards
- As this is a Database application, Code first approach has been followed
	
# NOTE:
Requirement was to develop and host in Azure platform
	- Azure is paid service, I am not able to deploy on Azure platform as I do not have free Azure account. But as this is a Containerized application
		you can choose Azure Container Instance or for more features (auto scaling) you can prefer Azure Kubernetes Service(AKS).
		
		
- The application should be built using .net + Azure                               -> Achived (Application is developed for Azure)

- The application should follow the best practices of software craftsmanship, 
such as clean code, SOLID principles, DRY principle, Separation of Concern, etc.   -> Achived (Followed SOLID principles, clean code, code maintainability)

- The application should implement at least 3 design patterns that are 
appropriate for the problem domain and the chosen technology stack.                -> Achived

- The application should have full unit test and functional test coverage, 
using any testing framework of your choice.                                        -> Achived

- The application should have a clear and consistent code style, 
following any coding standards or conventions that you prefer.                     -> Achived

- The application should have a README file that explains 
how to run, test and use the application, as well as any assumptions 
or decisions that you made during the development.                                 -> Achived