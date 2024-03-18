# NewDogTinder
The idea behind this project is to create a fake app to organize appointments between owners of dogs at the dog-park or at the beach etc.
Withe web API You can add an owner, a dog linked mandatorily to an owner, add a place, this will need to be able to create an appointment selecting a place, a dog, and a date. The appointments are the item more implemented you can delete, edit, or partially edit them.
This web API is a personal playground and not a production code.

## Technology used for the web API
For the web API I have used asp.net core version 6.0, using EF core code first, Microsoft SQL server, AutoMapper, Serilog and Swashbuckle to implement OpenApi.
A basic Authentication and Authorization process was implemented, the login is always valid, but a Barer Token is generated and used to be authenticated and Authorized, also an authorization process for the appointment controller is created (only the user with city claim of Trondheim are authorized).

## Testing Framework
Also, the testing part is only a playground and not an exhaustive test for production code. Some unit tests are implement using MSTest for the business logic (Service project) and the framework to test web API in memory is created and used for the appointment controller.
This integration framework is created using the Package Microsoft.AspNetCore.Mvc.Testing this package allow to create a custom WebApplicationFactory to override the setting in the Program file like datbase connection, configuration and authentication in this specific web API.
For the database is used respawn package to avoid creating a new datbase for each integration test:
"Respawn is a small utility to help in resetting test databases to a clean state. Instead of deleting data at the end of a test or rolling back a transaction, Respawn resets the database back to a clean, empty state by intelligently deleting data from tables." 
The results are excellent only the first test will require around 2 seconds to create the datbase after that respawn will made the test faster as unit test.
In the case the API will call a another API the IHttpClient can be spy or mock so to keep the avoid to test external dependencies in the integration test.
The philosophy used in this test is to try to follow a mix of London and Classic school of unit test.

## Working in progress…

