# shopping-microservice

run project
docker-compose down (to remove all containers)

move to folder contain docker-compose.yml

docker-compose -f docker-compose.override.yml -f docker-compose.yml up -d --remove-orphans

docker-compose -f docker-compose.override.yml -f docker-compose.yml up -d --remove-orphans --build


API local env

- product API: http://localhost:6002/swagger
- basket API: http://localhost:6070/swagger
- customer API: http://localhost:7091/swagger
- inventory API: http://localhost:7028/swagger
- ordering API: http://localhost:7090/swagger
- hangfire API: http://localhost:7022/swagger
- orderdb API: http://localhost:7035/swagger
- productdb API: http://localhost:7051/swagger
- customerdb API: http://localhost:7085/swagger
- basketdb API: http://localhost:7072/swagger
- inventorydb API: http://localhost:7099/swagger
- mongodb API: http://localhost:7077/swagger
- redis API: http://localhost:7087/swagger
- pgadmin API: http://localhost:5050/swagger
- portainer API: http://localhost:8080/swagger
- elasticsearch API: http://localhost:7095/swagger
- kibana API: http://localhost:7056/swagger

docker application URL
- product API: http://localhost:9000
- kibana API: http://localhost:5601 - username: elastic; password: admin
- rabbitmq API: http://localhost:15672 - username: guest; password: guest