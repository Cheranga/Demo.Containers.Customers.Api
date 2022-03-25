FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Customers.Api.csproj", "./"]
RUN dotnet restore "Customers.Api.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Customers.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customers.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customers.Api.dll"]

#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /app
#
#COPY Customers.Api.csproj ./
#RUN dotnet restore
#
#COPY . ./
#RUN dotnet publish "Customers.Api.csproj" -c Release -o out
#
#FROM mcr.microsoft.com/dotnet/aspnet:5.0
#WORKDIR /app
#COPY --from=build /app/out .
#EXPOSE 80
#EXPOSE 443
#ENTRYPOINT ["dotnet", "Customers.Api.dll"]