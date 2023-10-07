## Build the docker image

MacOS

```shell
version='1.0.1'

export GH_OWNER='iga1dotnetmicroservices'
export GH_PAT='[PAT HERE]'
docker build --secret id=GH_OWNER --secret id=GH_PAT -t play.trading:$version .
```

Windows Shell

```powershell
$version='1.0.1'

$env:GH_OWNER='iga1dotnetmicroservices'
$env:GH_PAT='[PAT HERE]'
docker build --secret id=GH_OWNER --secret id=GH_PAT -t play.trading:$version .
```

## Run the docker image

MacOS

```shell 
authority='[AUTHORITY]'
cosmosDbConnString='[CONN STRING HERE]'
serviceBusConnString='[CONN STRING HERE]'

docker run -it --rm -p 5006:5006 --name trading -e MongoDbSettings__ConnectionString=$cosmosDbConnString -e ServiceBusSettings__ConnectionString=$serviceBusConnString -e ServiceSettings__Authority=$authority -e ServiceSettings__MessageBroker="SERVICEBUS" play.trading:$version
```

Windows Shell

```powershell
$authority='[AUTHORITY]'
$cosmosDbConnString='[CONN STRING HERE]'
$serviceBusConnString='[CONN STRING HERE]'

docker run -it --rm -p 5006:5006 --name trading -e MongoDbSettings__ConnectionString=$cosmosDbConnString -e ServiceBusSettings__ConnectionString=$serviceBusConnString -e ServiceSettings__Authority=$authority -e ServiceSettings__MessageBroker="SERVICEBUS" play.trading:$version
```

