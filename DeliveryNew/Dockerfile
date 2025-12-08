# 1️⃣ Базовый образ
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

# 2️⃣ SDK образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["DeliveryNew.csproj", "./"]
RUN dotnet restore "./DeliveryNew.csproj"
COPY . .
RUN dotnet publish "DeliveryNew.csproj" -c Release -o /app

# 3️⃣ Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DeliveryNew.dll"]
