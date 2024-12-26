# Базовый образ для .NET приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Базовый образ для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Копируем все файлы и выполняем сборку
COPY ["src/Infrastructure/Tasker.Api/Tasker.Api.csproj", "src/Infrastructure/Tasker.Api/"]
COPY ["src/Infrastructure/Tasker.Discord/Tasker.Discord.csproj", "src/Infrastructure/Tasker.Discord/"]
RUN dotnet restore "src/Infrastructure/Tasker.Api/Tasker.Api.csproj"
COPY . .
WORKDIR "/src/src/Infrastructure/Tasker.Api"
RUN dotnet build "Tasker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Сборка для публикации
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Tasker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Открываем необходимые порты
EXPOSE 80
EXPOSE 443
EXPOSE 5000/udp

# Указываем команду запуска приложения
ENTRYPOINT ["dotnet", "Tasker.Api.dll"]
