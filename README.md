## Build the docker image

MacOS

```shell
version='1.0.2'
appname='iga1playeconomy'

export GH_OWNER='iga1dotnetmicroservices'
export GH_PAT='[PAT HERE]'
docker build --secret id=GH_OWNER --secret id=GH_PAT -t "$appname.azurecr.io/play.trading:$version" .
```

Windows Shell

```powershell
$version='1.0.2'
$appname='iga1playeconomy'

$env:GH_OWNER='iga1dotnetmicroservices'
$env:GH_PAT='[PAT HERE]'
docker build --secret id=GH_OWNER --secret id=GH_PAT -t "$appname.azurecr.io/play.trading:$version" .
```

## Run the docker image

MacOS

```shell 
authority='[AUTHORITY]'
cosmosDbConnString='[CONN STRING HERE]'
serviceBusConnString='[CONN STRING HERE]'

docker run -it --rm -p 5006:5006 --name trading -e MongoDbSettings__ConnectionString=$cosmosDbConnString -e ServiceBusSettings__ConnectionString=$serviceBusConnString -e ServiceSettings__Authority=$authority -e ServiceSettings__MessageBroker="SERVICEBUS" play.trading:$version
```

Windows

```powershell
$authority='[AUTHORITY]'
$cosmosDbConnString='[CONN STRING HERE]'
$serviceBusConnString='[CONN STRING HERE]'

docker run -it --rm -p 5006:5006 --name trading -e MongoDbSettings__ConnectionString=$cosmosDbConnString -e ServiceBusSettings__ConnectionString=$serviceBusConnString -e ServiceSettings__Authority=$authority -e ServiceSettings__MessageBroker="SERVICEBUS" play.trading:$version
```

## Publishing the docker image

MacOS

```shell
appname='iga1playeconomy'
az acr login --name $appname
docker push "$appname.azurecr.io/play.trading:$version"
```

Windows

```powershell
$appname='iga1playeconomy'
az acr login --name $appname
docker push "$appname.azurecr.io/play.trading:$version"
```

## Create the Kubernetes namespace

MacOS

```shell
namespace='trading'
kubectl create namespace $namespace
```

Windows

```powershell
$namespace='trading'
kubectl create namespace $namespace
```

## Create the pod managed identity

MacOS

```shell
az identity create --resource-group $appname --name $namespace

IDENTITY_RESOURCE_ID=$(az identity show -g $appname -n $namespace --query id -otsv)

az aks pod-identity add --resource-group $appname --cluster-name $appname --namespace $namespace --name $namespace --identity-resource-id $IDENTITY_RESOURCE_ID
```

Windows

```powershell
az identity create --resource-group $appname --name $namespace

$IDENTITY_RESOURCE_ID=az identity show -g $appname -n $namespace --query id -otsv

az aks pod-identity add --resource-group $appname --cluster-name $appname --namespace $namespace --name $namespace --identity-resource-id $IDENTITY_RESOURCE_ID
```

## Grant access to Key Vault secrets

```powershell
$IDENTITY_CLIENT_ID=az identity show -g $appname -n $namespace --query clientId -otsv
az keyvault set-policy -n $appname --secret-permissions get list --spn $IDENTITY_CLIENT_ID
```