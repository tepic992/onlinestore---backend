# 1️⃣ Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy remaining files and publish
COPY . ./
RUN dotnet publish -c Release -o out

# 2️⃣ Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published output
COPY --from=build /app/out .

# Environment variable za connection string (Render može override)
ENV DefaultConnection="Host=dpg-d4ms25buibrs738rsph0-a.oregon-postgres.render.com;Database=onlinestoredb_82cd;Username=onlinestoredb_82cd_user;Password=57JIE0e60sTAIg27ZiC4aDR3JYOBc27S;Port=5432"

# Expose port (Render automatski preusmerava, ali možeš staviti 10000)
EXPOSE 10000

# Start the app
ENTRYPOINT ["dotnet", "OnlineStore.Api.dll"]

