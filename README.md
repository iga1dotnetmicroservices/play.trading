## Build the docker image

MacOS

```shell
version='1.0.0'

export GH_OWNER='iga1dotnetmicroservices'
export GH_PAT='[PAT HERE]'
docker build --secret id=GH_OWNER --secret id=GH_PAT -t play.trading:$version .
```

Windows Shell

```powershell
$env:GH_OWNER='iga1dotnetmicroservices'
$env:GH_PAT='[PAT HERE]'
docker build --secret id=GH_OWNER --secret id=GH_PAT -t play.trading:$version .
```

## Run the docker image

MacOS

```shell 
authority='[AUTHORITY]'
docker run -it --rm -p 5006:5006 --name trading -e MongoDbSettings__Host=mongo -e RabbitMQSettings__Host=rabbitmq -e ServiceSettings__Authority=$authority --network playinfra_default play.trading:$version
```

Windows Shell

```powershell
$authority='[AUTHORITY]'
docker run -it --rm -p 5006:5006 --name trading -e MongoDbSettings__Host=mongo -e RabbitMQSettings__Host=rabbitmq -e ServiceSettings__Authority=$authority --network playinfra_default play.trading:$version
```

