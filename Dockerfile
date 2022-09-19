FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base

RUN dotnet tool install --global dotnet-ef --version 6.0.5 

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build

WORKDIR /src
COPY . .


RUN dotnet restore "Educative.Infrastructure/Educative.Infrastructure.csproj"
RUN dotnet restore "Educative.Core/Educative.Core.csproj"
RUN dotnet restore "Educative.API/Educative.API.csproj"
RUN dotnet restore "Educative.nUnitTests/Educative.nUnitTests.csproj"
COPY . .
WORKDIR "/src/Educative.API"



RUN dotnet build "Educative.API.csproj" -c Release -o /app/build

RUN dotnet watch run "Educative.API.csproj"

ENTRYPOINT ["dotnet", "Educative.API.dll"]

