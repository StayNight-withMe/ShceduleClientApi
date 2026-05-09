FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/ClientScheduleApi/ClientScheduleApi.csproj", "src/ClientScheduleApi/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Contracts/Contracts.csproj", "src/Contracts/"]

RUN dotnet restore "src/ClientScheduleApi/ClientScheduleApi.csproj"

COPY . .

WORKDIR "/src/src/ClientScheduleApi"
RUN dotnet publish "ClientScheduleApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8080;https://+:8081

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ClientScheduleApi.dll"]
