FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

COPY . ./

RUN dotnet restore src/CryptoBasket.sln
RUN dotnet publish src/CryptoBasket.sln -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/src/1.Presentation/CryptoBasket.Api/out/ .
ENTRYPOINT ["dotnet", "CryptoBasket.Api.dll"]