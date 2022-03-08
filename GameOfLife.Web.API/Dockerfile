#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GameOfLife.Web.API/GameOfLife.Web.API.csproj", "GameOfLife.Web.API/"]
RUN dotnet restore "GameOfLife.Web.API/GameOfLife.Web.API.csproj"
COPY . .
WORKDIR "/src/GameOfLife.Web.API"
RUN dotnet build "GameOfLife.Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameOfLife.Web.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameOfLife.Web.API.dll"]