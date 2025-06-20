# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore PracticalTest.sln
RUN dotnet publish PracticalTest.Api/PracticalTest.Api.csproj -c Release -o /app/publish
COPY PracticalTest.Api/Sales.txt ./

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "PracticalTest.Api.dll"] 
