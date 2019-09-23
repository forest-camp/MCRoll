FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["MCRoll.csproj", ""]
RUN dotnet restore "./MCRoll.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MCRoll.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MCRoll.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MCRoll.dll"]