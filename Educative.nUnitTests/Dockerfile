FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Educative.nUnitTests.csproj", "./"]
RUN dotnet restore "Educative.nUnitTests/Educative.nUnitTests.csproj"
COPY . .
WORKDIR "/src/Educative.nUnitTests"
RUN dotnet build "Educative.nUnitTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Educative.nUnitTests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Educative.nUnitTests.dll"]
