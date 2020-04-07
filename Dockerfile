FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore Easynvest.Api/*.csproj
RUN dotnet publish Easynvest.Api/*.csproj -c Release -o out

# Build da imagem
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Easynvest.Api.dll"]