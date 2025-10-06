FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["XoaiSay.sln","./"]
COPY ["src/XoaiSay.Domain.Shared/XoaiSay.Domain.Shared.csproj","src/XoaiSay.Domain.Shared/"]
COPY ["src/XoaiSay.Domain/XoaiSay.Domain.csproj","src/XoaiSay.Domain/"]
COPY ["src/XoaiSay.EntityFrameworkCore/XoaiSay.EntityFrameworkCore.csproj","src/XoaiSay.EntityFrameworkCore/"]
COPY ["src/XoaiSay.Application.Contracts/XoaiSay.Application.Contracts.csproj","src/XoaiSay.Application.Contracts/"]
COPY ["src/XoaiSay.Application/XoaiSay.Application.csproj","src/XoaiSay.Application/"]
COPY ["src/XoaiSay.HttpApi/XoaiSay.HttpApi.csproj","src/XoaiSay.HttpApi/"]
COPY ["src/XoaiSay.HttpApi.Host/XoaiSay.HttpApi.Host.csproj","src/XoaiSay.HttpApi.Host/"]
COPY ["src/XoaiSay.HttpApi.Client/XoaiSay.HttpApi.Client.csproj","src/XoaiSay.HttpApi.Client/"]
COPY ["src/XoaiSay.DbMigrator/XoaiSay.DbMigrator.csproj","src/XoaiSay.DbMigrator/"]

RUN dotnet restore "src/XoaiSay.HttpApi.Host/XoaiSay.HttpApi.Host.csproj"

COPY . .
RUN dotnet publish "src/XoaiSay.HttpApi.Host/XoaiSay.HttpApi.Host.csproj" -c Release -o /app/publish \
    && mkdir -p /app/publish/App_Data \
    && cp src/XoaiSay.HttpApi.Host/openiddict.pfx /app/publish/App_Data/openiddict.pfx \
    && cp src/XoaiSay.HttpApi.Host/openiddict.pfx /app/publish/openiddict.pfx \
    && dotnet publish "src/XoaiSay.DbMigrator/XoaiSay.DbMigrator.csproj" -c Release -o /app/publish/migrator

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","XoaiSay.HttpApi.Host.dll"]
