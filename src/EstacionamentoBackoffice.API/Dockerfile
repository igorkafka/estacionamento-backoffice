#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EstacionamentoBackoffice.API/EstacionamentoBackoffice.API.csproj", "src/EstacionamentoBackoffice.API/"]
COPY ["EstacionamentoBackoffice.Business/EstacionamentoBackoffice.Business.csproj", "src/EstacionamentoBackoffice.Business/"]
COPY ["EstacionamentoBackoffice.Data.csproj", "src/EstacionamentoBackoffice.Data/"]
RUN dotnet restore "src/EstacionamentoBackoffice.API/EstacionamentoBackoffice.API.csproj"
COPY . .
WORKDIR "/src/src/EstacionamentoBackoffice.API"
RUN dotnet build "EstacionamentoBackoffice.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EstacionamentoBackoffice.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EstacionamentoBackoffice.API.dll"]