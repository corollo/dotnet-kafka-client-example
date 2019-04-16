# dotnet-kafka-client-example

# start docker 
docker-compose up -d

# check docker 
docker ps

# stop docker
docker kill <container-id>

# compile e start dotnet client
dotnet build -c Release
dotnet bin\Release\netcoreapp2.1\KafkaConsumer.dll
