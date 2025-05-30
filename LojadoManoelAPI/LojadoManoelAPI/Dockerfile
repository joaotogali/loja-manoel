# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root 
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*
USER app

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LojadoManoelAPI/LojadoManoelAPI.csproj", "LojadoManoelAPI/"]
RUN dotnet restore "./LojadoManoelAPI/LojadoManoelAPI.csproj"
COPY . .
WORKDIR "/src/LojadoManoelAPI"
RUN dotnet build "./LojadoManoelAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LojadoManoelAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY healthcheck.sh /healthcheck.sh

USER root
RUN chmod +x /healthcheck.sh
USER app

ENTRYPOINT ["dotnet", "LojadoManoelAPI.dll"]