FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["App.Consumer/App.Consumer.csproj", "App.Consumer/"]
RUN dotnet restore "App.Consumer/App.Consumer.csproj"
COPY . .
WORKDIR "/src/App.Consumer"
RUN dotnet build "App.Consumer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "App.Consumer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "App.Consumer.dll"]