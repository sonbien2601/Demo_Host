#!/bin/sh
set -eu

if [ "${SKIP_DB_MIGRATION:-0}" != "1" ]; then
  echo "Running database migrations..."
  dotnet migrator/XoaiSay.DbMigrator.dll
else
  echo "Skipping database migrations because SKIP_DB_MIGRATION=1"

if [ -d "/app/wwwroot/libs/abp" ]; then
  ls /app/wwwroot/libs/abp || true
else
echo "Starting XoaiSay.HttpApi.Host..."
exec dotnet XoaiSay.HttpApi.Host.dll

