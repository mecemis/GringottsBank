#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Identity/GringottsBank.IdentityServer/GringottsBank.IdentityServer.csproj", "src/Identity/GringottsBank.IdentityServer/"]
COPY ["src/Shared/GringottsBank.Shared/GringottsBank.Shared.csproj", "src/Shared/GringottsBank.Shared/"]
RUN dotnet restore "src/Identity/GringottsBank.IdentityServer/GringottsBank.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/Identity/GringottsBank.IdentityServer"
RUN dotnet build "GringottsBank.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GringottsBank.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GringottsBank.IdentityServer.dll"]