#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443

# Build
#FROM mcr.microsoft.com/dotnet/sdk:6.0-buster-slim AS publish
FROM portus.rossko.ru/dotnet/sdk:6.0 as publish
COPY . /app
WORKDIR /app/Rossko.ElasticWebForm.Web

RUN dotnet publish -c Release -o /app/out

#Build Runtime
#FROM mcr.microsoft.com/dotnet/aspnet:6.0-buster-slim
FROM portus.rossko.ru/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=publish /app/out ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "Rossko.ElasticWebForm.Web.dll"]

RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*
