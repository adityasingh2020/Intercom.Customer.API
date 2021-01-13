# Intercom Customer API Test

Output file "output.json" is located at root folder 

How to run and test 

A- Using Docker

Pre-requisite : Install Docker ( https://docs.docker.com/install/ )

1- Download/clone the code & Navigate to folder ( root ) conatining Dockerfile 

2- docker build -t customerapi .

3- Dockerfile has a step to execute unit test cases and will be executed in above steps i.e. while creating docker image.
   Test result will be displayed on console after executing the step 2

4- docker run -d -p 5001:80 --name custapi customerapi

5- Open http://localhost:5001/index.html in browser

6- Execute ~GetNearbyCustomers endpoint using swagger UI 
   or execute 
   curl -X GET "http://localhost:5001/api/v1/GetNearbyCustomers" -H "accept: application/json"
   curl -X GET "http://localhost:5001/api/v1/GetNearbyCustomers?distance=100" -H "accept: application/json"
   curl -X GET "http://localhost:5001/HealthCheck" -H  "accept: */*"
   

B- download & Install .NET Core 3.1

https://dotnet.microsoft.com/download/dotnet-core/3.1

Build
- dotnet build
Run Test Cases
- dotnet test -v n

Run Application
- change directory to api folder
- ~C:\....\InterCom.Customer.API\InterCom.Customer.API> dotnet run

Query endpoints 
  using swagger :
  Open http://localhost:5001/index.html in browser
  Execute ~GetNearbyCustomers endpoint using swagger UI 
  
  or excecure curl command as below
- curl -X GET "http://localhost:5001/api/v1/GetNearbyCustomers" -H "accept: application/json"
- curl -X GET "http://localhost:5001/api/v1/GetNearbyCustomers?distance=100" -H "accept: application/json"
- curl -X GET "https://localhost:5001/HealthCheck" -H  "accept: */*"



