# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Ordering/Order.API/Order.API.csproj", "Services/Ordering/Order.API/"]
COPY ["Services/Ordering/Order.Application/Order.Application.csproj", "Services/Ordering/Order.Application/"]
COPY ["Services/Ordering/Order.Core/Order.Core.csproj", "Services/Ordering/Order.Core/"]
COPY ["Services/Ordering/Order.Infrastructure/Order.Infrastructure.csproj", "Services/Ordering/Order.Infrastructure/"]
RUN dotnet restore "./Services/Ordering/Order.API/Order.API.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/Order.API"
RUN dotnet build "./Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.API.dll"]