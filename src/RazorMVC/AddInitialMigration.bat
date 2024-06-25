dotnet tool install --global dotnet-ef
dotnet tool restore
dotnet ef database drop
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update