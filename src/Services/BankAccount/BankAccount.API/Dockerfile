#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/BankAccount/BankAccount.API/BankAccount.API.csproj", "src/Services/BankAccount/BankAccount.API/"]
COPY ["src/Services/BankAccount/BankAccount.Infrastructure/BankAccount.Infrastructure.csproj", "src/Services/BankAccount/BankAccount.Infrastructure/"]
COPY ["src/Services/BankAccount/BankAccount.Domain/BankAccount.Domain.csproj", "src/Services/BankAccount/BankAccount.Domain/"]
COPY ["src/Services/BankAccount/BankAccount.Application/BankAccount.Application.csproj", "src/Services/BankAccount/BankAccount.Application/"]
COPY ["src/Shared/GringottsBank.Shared/GringottsBank.Shared.csproj", "src/Shared/GringottsBank.Shared/"]
RUN dotnet restore "src/Services/BankAccount/BankAccount.API/BankAccount.API.csproj"
COPY . .
WORKDIR "/src/src/Services/BankAccount/BankAccount.API"
RUN dotnet build "BankAccount.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankAccount.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankAccount.API.dll"]
