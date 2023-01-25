#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/MasterDataManagement/MasterDataManagement.API/MasterDataManagement.API.csproj", "Services/MasterDataManagement/MasterDataManagement.API/"]
COPY ["Services/MasterDataManagement/MasterDataManagement.Infrastructure/MasterDataManagement.Infrastructure.csproj", "Services/MasterDataManagement/MasterDataManagement.Infrastructure/"]
COPY ["Services/MasterDataManagement/MasterDataManagement.Core/MasterDataManagement.Core.csproj", "Services/MasterDataManagement/MasterDataManagement.Core/"]
RUN dotnet restore "Services/MasterDataManagement/MasterDataManagement.API/MasterDataManagement.API.csproj"
COPY . .
WORKDIR "/src/Services/MasterDataManagement/MasterDataManagement.API"
RUN dotnet build "MasterDataManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasterDataManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasterDataManagement.API.dll"]