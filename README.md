# ExampleApp

To create a signed development certificate:

https://learn.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-7.0

dotnet dev-certs https -ep ${HOME}/.aspnet/https/ExampleApp.pfx -p MySecurePwd1@
dotnet dev-certs https --trust

environment:
  - ASPNETCORE_Kestrel__Certificates__Default__Password=MySecurePwd1@
  - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/ExampleApp.pfx
