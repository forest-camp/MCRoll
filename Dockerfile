FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MCRoll.csproj", ""]
RUN dotnet restore "./MCRoll.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MCRoll.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MCRoll.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MCRoll.dll"]