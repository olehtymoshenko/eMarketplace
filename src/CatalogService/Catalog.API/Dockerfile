#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
# Configure HTTPS
# 1) dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p <CREDENTIAL_PLACEHOLDER>
# 2) dotnet dev-certs https --trust
# Run container: 
# -e ASPNETCORE_Kestrel__Certificates__Default__Password="<CREDENTIAL_PLACEHOLDER>" 
# -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx 
# -v %USERPROFILE%\.aspnet\https:/https/ 
# <Image_Name>

# docker run -p 5050:8080 -p 5051:8081 -e ASPNETCORE_Kestrel__Certificates__Default__Password="root" -e ASPNETCORE_Kestrel__Certificates__Default__Path="/https/Catalog.API.pfx" -e ASPNETCORE_ENVIRONMENT=Development -v %USERPROFILE%\.aspnet\https:/https/ catalog.api.deploy.build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
ENV ASPNETCORE_HTTP_PORTS=8080
ENV ASPNETCORE_HTTPS_PORTS=8081
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
EXPOSE 8081
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CatalogService/Catalog.API/Catalog.API.csproj", "CatalogService/Catalog.API/"]
COPY ["CatalogService/Catalog.Business/Catalog.Business.csproj", "CatalogService/Catalog.Business/"]
COPY ["CatalogService/Catalog.Persistence/Catalog.Persistence.csproj", "CatalogService/Catalog.Persistence/"]
RUN dotnet restore "./CatalogService/Catalog.API/Catalog.API.csproj"

COPY ./CatalogService ./CatalogService
WORKDIR "/src/CatalogService/Catalog.API"
RUN dotnet publish "Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false 

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT dotnet Catalog.API.dll --no-build