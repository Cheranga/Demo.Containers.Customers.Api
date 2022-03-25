# Customers API

## Introduction

An ASP.NET Core 5 Web API to manage customers.

## :bulb: Goals

- [x] Vertical sliced architecture
- [ ] Dapper as ORM
- [ ] Use CI/CD to deploy as a container to ACI
- [ ] Use CI/CD to deploy as a container to AKS

###  :bulb: Goal - Vertical sliced architecture

- [x] Setup controller inside the respective feature
- [x] Setup the mediators inside their respective feature
- [x] Wire up the relationship between controllers

###  :bulb: Goal - Dapper as ORM

- [ ] Run `SQL Server` in a docker container
- [ ] Test connectivity to the local running container
- [ ] Create commands and queries
- [ ] Modify the services to use the commands and queries
- [ ] Test commands and queries
- [ ] Use docker compose to run both API and the SQL server containers


## References

* Building an ASP.NET Core Web API with HTTPS and Docker

  * [Microsoft](https://github.com/dotnet/dotnet-docker/blob/main/samples/run-aspnetcore-https-development.md)
  * [YouTube Les Jackson](https://www.youtube.com/watch?v=lcaDDxJv260&list=PLpSmZmoBaROavSAfqx0k-c27i2m3P58oD)

* Running the Docker container with HTTPS command,

```dockerfile
docker run --rm -it -p 8080:80 -p 8081:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8081 -e ASPNETCORE_ENVIRONMENT=Development -v $env:APPDATA\microsoft\UserSecrets\:/root/.microsoft/usersecrets -v $env:USERPROFILE\.aspnet\https:/root/.aspnet/https/ cheranga/customersapi:v1.0.0
```
