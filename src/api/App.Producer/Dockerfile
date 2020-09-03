FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["App.Producer/App.Producer.csproj", "App.Producer/"]
RUN dotnet restore "App.Producer/App.Producer.csproj"
COPY . .
WORKDIR "/src/App.Producer"
RUN dotnet build "App.Producer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "App.Producer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "App.Producer.dll"]