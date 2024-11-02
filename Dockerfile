FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RedarborEmployees.Web/RedarborEmployees.Web.csproj", "RedarborEmployees.Web/"]
RUN dotnet restore "RedarborEmployees.Web/RedarborEmployees.Web.csproj"
COPY . .
WORKDIR "/src/RedarborEmployees.Web"
RUN dotnet build "RedarborEmployees.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedarborEmployees.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedarborEmployees.Web.dll"]