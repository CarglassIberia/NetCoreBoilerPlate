FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80/TCP
EXPOSE 443/TCP

FROM mcr.microsoft.com/dotnet/core/sdk:2.2.300-stretch AS build

WORKDIR /app
COPY ["Directory.Build.props", "."]
COPY ["src/Boilerplate.Host/Boilerplate.Host.csproj", "src/Boilerplate.Host/"]
COPY ["src/Boilerplate.API/Boilerplate.API.csproj", "src/Boilerplate.API/"]

RUN dotnet restore "src/Boilerplate.API/Boilerplate.API.csproj"
RUN dotnet restore "/app/src/Boilerplate.Host/Boilerplate.Host.csproj"
COPY . .

WORKDIR /app/src/Boilerplate.Host
RUN ls -la 
RUN pwd
RUN dotnet build "Boilerplate.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Boilerplate.Host.csproj" -c Release --no-restore -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Boilerplate.Host.dll"]