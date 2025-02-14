# 使用 ASP.NET 8.0 作為基底映像
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 使用 SDK 進行編譯
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CathayInterviewAPI.csproj", "."]
RUN dotnet restore "./CathayInterviewAPI.csproj"
COPY . . 
WORKDIR "/src/."
RUN dotnet build "./CathayInterviewAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 進行發佈
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CathayInterviewAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 最終映像
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY mock_response.json currencies_chinese_name.json /app/
ENTRYPOINT ["dotnet", "CathayInterviewAPI.dll"]
