# MicrosoftSummerInternship2021-IC3-IDC
My work accross 8 weeks @Microsoft IDC Office for IC3 Config Admin Scenario ! 

## Codebase Setup 
- Firstly , given the permissions of the codebase , the following command can be executed in the terminal : 
    https://github.com/NiharikaVadlamudi/MicrosoftSummerInternship2021-IC3-IDC.git
- NuGet Package Installation : 
  - Microsoft Azure DocumentDB Core (2.14.1)
  - Newtonsoft Json (13.0.1) 
- Launch Postman , to test various WEB APIs ( Few cases under DEMO Folder ) 
    - https://app.getpostman.com/join-team?invite_code=fc90c76446ba44c81591b87bb766ecf2 

## Week 2 
**Target** : Create  a  sample database (proper schema) on Azure Cosmos DB and create  simple REST API using ASP.Net Core.
Operations Available : 
- GET (Users)
    - Fetches all the users present in the database . 
- GET (by Department) 
    - Group by Departments (Eg:SovCloudHyd)
- POST 
    - Add new user to the database . 
- DELETE 
   - Delete the selected user (via ID) . 
## Week 3 & 4 
**Target** : Combination of Entity Framework 5.0 & OData 8.0 , for adding filtering & searching support. It supports the following functionalities : 
- GET (Users)
    - Fetches all the users present in the database . 
- GET (by id) 
    - Returns the user by tenantId . 
- POST 
    - Add new user to the database .
- DELETE 
   - Delete the selected user (via ID) . 
- PUT 
   -Updates the user record. 
## Week 5 & 6 : 
**Target** : Building Data Ingestion Tool , on top of Cosmos DB Bulk Insert API that takes an input configuration file with tenant name as key , and tenant size as the value . 
To run the tool , you can following steps : 
* git clone https://github.com/NiharikaVadlamudi/MicrosoftSummerInternship2021-IC3-IDC/tree/main/DataGeneratorTool
* Edit the program.cs file with relevant database name & primary connection string . 
* On your terminal : $ dotnet run 
## Week 7,8 : 
**Target** : Connecting the service to Application Insights & Azure Monitor , for observing telemetry which will aid in understanding the patterns of cost & detect APIs with high COGS value .
To run the tool , you can following steps : 
* git clone https://github.com/NiharikaVadlamudi/MicrosoftSummerInternship2021-IC3-IDC/tree/main/UserTenantAPIDeployement/UserTenantTestAPI
* Edit the program.cs,usercontext.cs & application.jsons file with relevant database name & primary connection string of the Application Insights tool . 
* On your terminal : $ dotnet run





