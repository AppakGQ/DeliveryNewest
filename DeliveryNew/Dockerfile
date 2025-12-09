# ---------------------------
# Stage 1: Build
# ---------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1️⃣ Копируем solution и csproj(ы)
COPY *.sln ./
COPY DeliveryNew/*.csproj ./DeliveryNew/

# 2️⃣ Restore через solution
RUN dotnet restore

# 3️⃣ Копируем весь исходный код
COPY . .

# 4️⃣ Publish проекта
RUN dotnet publish "DeliveryNew/DeliveryNew.csproj" -c Release -o /app/publish

# ---------------------------
# Stage 2: Runtime
# ---------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# 5️⃣ Настройка порта (если приложение слушает 5000)
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

# 6️⃣ Копируем только готовую сборку
COPY --from=build /app/publish ./

# 7️⃣ Запуск приложения
ENTRYPOINT ["dotnet", "DeliveryNew.dll"]
