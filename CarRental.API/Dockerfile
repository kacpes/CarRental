#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CarRental.API/CarRental.API.csproj", "CarRental.API/"]
RUN dotnet restore "CarRental.API/CarRental.API.csproj"
COPY . .
WORKDIR "/src/CarRental.API"
RUN dotnet build "CarRental.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarRental.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRental.API.dll"]