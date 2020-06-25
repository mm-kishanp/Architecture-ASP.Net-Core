# Architecture-ASP.Net-Core

This architecture is made of several project with different purpose.
Projects are as per below.

1) Architecture.Web
2) Architecture.BusinessLogic
3) Architecture.Data
4) Architecture.Entities
5) Architecture.ILogger
6) Architecture.Interface
7) Architecture.Repository
8) Architecture.Services
9) Architecture.Utility
10) Architecture.UnitTesting

**1.**	**Architecture.Web**
Maps to the layers that hold the Web, UI and Presenter concerns. In the context of our API, this means it accepts input in the form of http requests over the network (e.g., GET/POST/etc.) and returns its output as content formatted as JSON/HTML/XML, etc. The Presenters contain .NET framework-specific references and types, so they also live here as per The Dependency Rule we don't want to pass any of them inward.

**2.**	**Architecture.BusinessLogic**
Maps to the business layer which hold the actual Business logic. Where we have defined all the conditions and loops to convert data to object. Map to any entities, etc. 
This Project is pure C# project. Which has no network connections database connections allowed. Interface represent those dependencies and their implementations get injected into our use cases.

**3.**	**Architecture.Database**
This Project is pure C# project. Which has no network connections database connections allowed. Interface represent those dependencies and their implementations get injected into our use cases.

**4.**	**Architecture.Entities**
This Project is pure C# project. This will have requested and response view models entities. Which will be used in the businesslogic project. All models and ViewModels are used for UI references only. This Models are not connected with Database.

**5.**	**Architecture.ILogger**
This Project is pure C# project. Thi will be used to for logging and will generate logs as a text file.

**6.**	**Architecture.Interface**
This Project is pure C# project. This project doesn’t have any implementation. It will only have interface files which is going to use for repositories. No implementation in this project only method schema will be stored in this project.

**7.**	**Architecture.Repository**
This Project is pure C# project. This project has the actual logic to fetch data from the Database. Actual implementation of getting data and giving data to business layer is done from this project. 

**8.**	**Architecture.Services**
This Project is pure C# project. Which may have 3rd party integrations implemented. Used any services for send mail or send SMS Payment gateway services etc.

**9.**	**Architecture.Utility**
This project is pure C# project. It has any generic methods and common methods like serializing deserializing, XML conversion, JSON conversion, Encryption, Description, Random number generation etc. 

**10.**	**Architecture.UnitTesting.**
This project is developed for applying unit testing to the business logic. It's useful to test how our controller behaves based on the validity of the inputs and validate its response based on the result of the operation it performs
