#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 3000
ENV ASPNETCORE_URLS=http://+:3000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SwaggerAggregator/SwaggerAggregator.csproj", "src/SwaggerAggregator/"]
RUN dotnet restore "src/SwaggerAggregator/SwaggerAggregator.csproj"
COPY . .
WORKDIR "/src/SwaggerAggregator"
RUN dotnet build "SwaggerAggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SwaggerAggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SwaggerAggregator.dll"]
