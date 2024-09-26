#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RealEstateListingAPI/RealEstateListingApi.csproj", "RealEstateListingAPI/"]
COPY ["RealEstateListingApi.Application/RealEstateListingApi.Application.csproj", "RealEstateListingApi.Application/"]
COPY ["RealEstateListingApi.Domain/RealEstateListingApi.Domain.csproj", "RealEstateListingApi.Domain/"]
COPY ["RealEstateListingApi.Infrastructure/RealEstateListingApi.Infrastructure.csproj", "RealEstateListingApi.Infrastructure/"]
RUN dotnet restore "./RealEstateListingAPI/RealEstateListingApi.csproj"
COPY . .
WORKDIR "/src/RealEstateListingAPI"
RUN dotnet build "./RealEstateListingApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RealEstateListingApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealEstateListingApi.dll"]