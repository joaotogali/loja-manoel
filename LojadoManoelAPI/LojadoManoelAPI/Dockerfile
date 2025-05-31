FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LojadoManoelAPI/LojadoManoelAPI.csproj", "LojadoManoelAPI/"]
RUN dotnet restore "LojadoManoelAPI/LojadoManoelAPI.csproj"
COPY . .
WORKDIR "/src/LojadoManoelAPI"
RUN dotnet build "LojadoManoelAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LojadoManoelAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LojadoManoelAPI.dll"]