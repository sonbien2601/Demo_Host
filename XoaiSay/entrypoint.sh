#!/bin/sh
set -euo pipefail

if [ "${SKIP_DB_MIGRATION:-0}" != "1" ]; then
  echo "Running database migrations..."
  dotnet migrator/XoaiSay.DbMigrator.dll
else
  echo "Skipping database migrations because SKIP_DB_MIGRATION=1"
fi

echo "Starting XoaiSay.HttpApi.Host..."
exec dotnet XoaiSay.HttpApi.Host.dll