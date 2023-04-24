#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:8000;http://+:80;
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TomTec.RoundBuy.API/TomTec.RoundBuy.API.csproj", "TomTec.RoundBuy.API/"]
COPY ["TomTec.RoundBuy.Business/TomTec.RoundBuy.Business.csproj", "TomTec.RoundBuy.Business/"]
COPY ["TomTec.RoundBuy.Lib.Utils/TomTec.RoundBuy.Lib.Utils.csproj", "TomTec.RoundBuy.Lib.Utils/"]
COPY ["TomTec.RoundBuy.Models/TomTec.RoundBuy.Models.csproj", "TomTec.RoundBuy.Models/"]
COPY ["TomTec.RoundBuy.Data/TomTec.RoundBuy.Data.csproj", "TomTec.RoundBuy.Data/"]
COPY ["TomTec.RoundBuy.Lib.AspNetCore/TomTec.RoundBuy.Lib.AspNetCore.csproj", "TomTec.RoundBuy.Lib.AspNetCore/"]
RUN dotnet restore "TomTec.RoundBuy.API/TomTec.RoundBuy.API.csproj"
COPY . .
WORKDIR "/src/TomTec.RoundBuy.API"
RUN dotnet build "TomTec.RoundBuy.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TomTec.RoundBuy.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TomTec.RoundBuy.API.dll"]