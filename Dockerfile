# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

ARG CERT_PWD=admin@123

COPY ["XoaiSay/XoaiSay.sln", "./"]
COPY ["XoaiSay/src/XoaiSay.Domain.Shared/XoaiSay.Domain.Shared.csproj", "src/XoaiSay.Domain.Shared/"]
COPY ["XoaiSay/src/XoaiSay.Domain/XoaiSay.Domain.csproj", "src/XoaiSay.Domain/"]
COPY ["XoaiSay/src/XoaiSay.EntityFrameworkCore/XoaiSay.EntityFrameworkCore.csproj", "src/XoaiSay.EntityFrameworkCore/"]
COPY ["XoaiSay/src/XoaiSay.Application.Contracts/XoaiSay.Application.Contracts.csproj", "src/XoaiSay.Application.Contracts/"]
COPY ["XoaiSay/src/XoaiSay.Application/XoaiSay.Application.csproj", "src/XoaiSay.Application/"]
COPY ["XoaiSay/src/XoaiSay.HttpApi/XoaiSay.HttpApi.csproj", "src/XoaiSay.HttpApi/"]
COPY ["XoaiSay/src/XoaiSay.HttpApi.Host/XoaiSay.HttpApi.Host.csproj", "src/XoaiSay.HttpApi.Host/"]
COPY ["XoaiSay/src/XoaiSay.HttpApi.Client/XoaiSay.HttpApi.Client.csproj", "src/XoaiSay.HttpApi.Client/"]
COPY ["XoaiSay/src/XoaiSay.DbMigrator/XoaiSay.DbMigrator.csproj", "src/XoaiSay.DbMigrator/"]

RUN dotnet restore "src/XoaiSay.HttpApi.Host/XoaiSay.HttpApi.Host.csproj"

COPY XoaiSay/. .

RUN dotnet tool install -g Volo.Abp.Cli \
    && export PATH="$PATH:/root/.dotnet/tools" \
    && cd src/XoaiSay.HttpApi.Host \
    && abp install-libs \
    && dotnet publish XoaiSay.HttpApi.Host.csproj -c Release -o /app/publish \
    && mkdir -p /app/publish/App_Data \
    && cp /tmp/openiddict.pfx /app/publish/App_Data/openiddict.pfx \
    && cp /tmp/openiddict.pfx /app/publish/openiddict.pfx \
    && cd /src \
    && dotnet publish src/XoaiSay.DbMigrator/XoaiSay.DbMigrator.csproj -c Release -o /app/publish/migrator

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
COPY --from=build /app/publish .
COPY --from=build /src/entrypoint.sh ./entrypoint.sh
RUN chmod +x ./entrypoint.sh
ENTRYPOINT ["./entrypoint.sh"]
