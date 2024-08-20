# shopping-microservice

run project
docker-compose down (to remove all containers)

move to folder contain docker-compose.yml

docker-compose -f docker-compose.override.yml -f docker-compose.yml up -d --remove-orphans

docker-compose -f docker-compose.override.yml -f docker-compose.yml up -d --remove-orphans --build


API local env

- product API: http://localhost:6002/swagger
- basket API: http://localhost:6004/swagger
- customer API: http://localhost:6003/swagger

migration command
- dotnet ef migrations add "MigrationName" --project .\Ordering.Infrastructure\ --startup-project .\Ordering.API\ --output-dir Persistence\Migrations

update database command
- dotnet ef database update --project .\Ordering.Infrastructure\ --startup-project .\Ordering.API\