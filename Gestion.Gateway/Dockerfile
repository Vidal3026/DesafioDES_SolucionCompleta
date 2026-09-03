FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Gestion.Gateway/Gestion.Gateway.csproj", "Gestion.Gateway/"]
RUN dotnet restore "Gestion.Gateway/Gestion.Gateway.csproj"

COPY . .
WORKDIR "/src/Gestion.Gateway"
RUN dotnet build "Gestion.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gestion.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gestion.Gateway.dll"]