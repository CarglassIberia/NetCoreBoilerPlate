FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80/TCP
EXPOSE 443/TCP
FROM mcr.microsoft.com/dotnet/core/sdk:2.2.300-stretch AS build
WORKDIR /src
COPY ["src/Boilerplate.Host/Boilerplate.Host.csproj", "src/Boilerplate.Host/"]
COPY ["src/Boilerplate.API/Boilerplate.API.csproj", "src/Boilerplate.API/"]
RUN dotnet restore "src/Boilerplate.Host/Boilerplate.Host.csproj"
COPY . .
WORKDIR "/src/src/Boilerplate.Host"
RUN dotnet build "Boilerplate.Host.csproj" --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Boilerplate.Host.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Boilerplate.Host.dll"]