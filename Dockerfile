FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.18 AS base
RUN apk update && \
    apk add --no-cache icu-libs tzdata libgdiplus fontconfig && \
    rm -rf /var/cache/apk/*
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
EXPOSE 5179

ENV ASPNETCORE_URLS=http://+:5179
ENV ASPNETCORE_ENVIRONMENT=Development

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.18 AS build
WORKDIR /src
ARG AZURETOKEN_DEFAULE
COPY ["StockManagementSystem.csproj", "./"]
COPY . .
RUN dotnet restore "StockManagementSystem.csproj"
WORKDIR "/src/."
RUN dotnet build "StockManagementSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockManagementSystem.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV TZ="Asia/Bangkok"
WORKDIR /app

# Create data directory and set permissions
USER root
RUN mkdir -p /app/data && chown -R appuser:appuser /app/data
USER appuser

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockManagementSystem.dll"]
