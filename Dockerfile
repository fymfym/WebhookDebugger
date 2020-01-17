#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["WebhookDebugger/WebhookDebugger.csproj", "WebhookDebugger/"]
RUN dotnet restore "WebhookDebugger/WebhookDebugger.csproj"
COPY . .
WORKDIR "/src/WebhookDebugger"
RUN dotnet build "WebhookDebugger.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebhookDebugger.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebhookDebugger.dll"]