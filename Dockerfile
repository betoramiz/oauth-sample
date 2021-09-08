#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 5000
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
#WORKDIR /src
#COPY ["Floreria.Api.csproj", "./"]
#RUN dotnet restore "Floreria.Api.csproj"
#COPY . .
#WORKDIR /src
#RUN dotnet build "Floreria.Api.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Floreria.Api.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Floreria.Api.dll"]


FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .
COPY Api/*.csproj ./Api/
COPY Api.Database/*.csproj ./Api.Database/
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build as publish
RUN dotnet publish -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
