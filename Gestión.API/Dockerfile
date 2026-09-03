FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto para restaurar dependencias
COPY ["Gestión.API/Gestión.API.csproj", "Gestión.API/"]
COPY ["Gestion.BL/Gestion.BL.csproj", "Gestion.BL/"]
COPY ["Gestion.Common/Gestion.Common.csproj", "Gestion.Common/"]
COPY ["Gestion.DAL/Gestion.DAL.csproj", "Gestion.DAL/"]
COPY ["Gestion.Entities/Gestion.Entities.csproj", "Gestion.Entities/"]

RUN dotnet restore "Gestión.API/Gestión.API.csproj"

# Copiar todo el código fuente y compilar
COPY . .
WORKDIR "/src/Gestión.API"
RUN dotnet build "Gestión.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gestión.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gestión.API.dll"]