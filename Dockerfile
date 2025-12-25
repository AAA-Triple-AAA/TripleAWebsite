# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["TripleAWebsite.Frontend/TripleAWebsite.Frontend.csproj", "TripleAWebsite.Frontend/"]
RUN dotnet restore "TripleAWebsite.Frontend/TripleAWebsite.Frontend.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/TripleAWebsite.Frontend"
RUN dotnet build "TripleAWebsite.Frontend.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "TripleAWebsite.Frontend.csproj" -c Release -o /app/publish

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .

# Create a directory for the database to ensure permissions exist
RUN mkdir -p /app/data
# Create a directory for uploads
RUN mkdir -p /app/wwwroot/uploads

ENTRYPOINT ["dotnet", "TripleAWebsite.Frontend.dll"]
