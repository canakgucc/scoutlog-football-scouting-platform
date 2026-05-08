# ScoutLog

ScoutLog is a football club scouting demo platform built with ASP.NET Core Web API, Entity Framework Core, SQL Server, JWT authentication, and Angular.

## Configuration

Do not commit real SQL passwords or JWT secrets. `ScoutLog.API/appsettings.json` contains only safe placeholders. Use `ScoutLog.API/appsettings.example.json` as a reference.

For local development, set secrets with user-secrets:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=ScoutLogDb;User Id=sa;Password=<YOUR_SQL_PASSWORD>;TrustServerCertificate=True;" --project ScoutLog.API
dotnet user-secrets set "Jwt:Key" "<A_RANDOM_SECRET_WITH_AT_LEAST_32_CHARACTERS>" --project ScoutLog.API
```

Environment variable alternatives:

```bash
export ConnectionStrings__DefaultConnection="Server=localhost,1433;Database=ScoutLogDb;User Id=sa;Password=<YOUR_SQL_PASSWORD>;TrustServerCertificate=True;"
export Jwt__Key="<A_RANDOM_SECRET_WITH_AT_LEAST_32_CHARACTERS>"
```

## SQL Server Docker

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YOUR_SQL_PASSWORD>" -p 1433:1433 --name scoutlog-sql -d mcr.microsoft.com/mssql/server:2022-latest
```

Use the same password in your local connection string.

## Database

When using EF design-time commands, expose the connection string for the design-time factory:

```bash
export SCOUTLOG_CONNECTION_STRING="Server=localhost,1433;Database=ScoutLogDb;User Id=sa;Password=<YOUR_SQL_PASSWORD>;TrustServerCertificate=True;"
dotnet ef database update --project ScoutLog.Infrastructure --startup-project ScoutLog.API
```

## Run

Backend:

```bash
dotnet run --project ScoutLog.API
```

Swagger:

```text
http://localhost:5198/swagger
```

Frontend:

```bash
cd ScoutLog.Client
npm install
npm start
```

Angular app:

```text
http://localhost:4200
```

## Demo Users

```text
scout@fenerbahce.local / Demo123!
scout@galatasaray.local / Demo123!
scout@besiktas.local / Demo123!
```

## Security Notes

- Public registration is limited to Development environment only.
- JWT secrets and SQL connection strings must come from user-secrets or environment variables.
- Before pushing, verify no generated `bin/`, `obj/`, `dist/`, `.env`, or production appsettings files are staged.
