#DOCKER FILE PARA DEPLOY DA APLICAÇÃO DE API

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1  AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build

WORKDIR /src
COPY fiap73aoj.servicearchitecture.API.sln ./
COPY ["App.Consumer/*.csproj", "./App.Consumer/"]
COPY ["App.Application/*.csproj", "./App.Application/"]
COPY ["App.Domain/*.csproj", "./App.Domain/"]
COPY ["App.Infra/*.csproj", "./App.Infra/"]


RUN dotnet restore 


COPY App.Consumer/. ./App.Consumer/
COPY App.Application/. ./App.Application/
COPY App.Infra/. ./App.Infra/ 
COPY App.Domain/. ./App.Domain/ 

WORKDIR /src/App.Consumer 
#FROM build AS publish
RUN dotnet build -c Release -o /app
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "App.Consumer.dll"]
