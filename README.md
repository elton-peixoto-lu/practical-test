# Practical Test - Transaction API

This is a RESTful API built with .NET Core that manages a list of transactions. The API uses Dapper for data access, implements CQRS pattern, and uses Oracle as the database.

## Prerequisites

- .NET 7.0 SDK
- Docker and Docker Compose (for running Oracle Database)
- Visual Studio 2022 or VS Code
- Heroku CLI (for deployment)

## Setup

### Option 1: Using Docker (Recommended for Local Development)

1. Clone the repository
2. Start the Oracle Database container:
```bash
docker-compose up -d
```
3. Wait for the Oracle container to be ready (it may take a few minutes on first run)
4. Update the connection string in `appsettings.json` with:
```json
"OracleConnection": "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=system;Password=your_password;"
```
5. Run the SQL script in `PracticalTest.Api/Data/Scripts/CreateTable.sql` to create the Transactions table
6. Restore NuGet packages
7. Build the solution

### Option 2: Deploying to Heroku (Recommended for Online Development)

1. Create a free Heroku account at https://signup.heroku.com/

2. Install Heroku CLI:
```bash
# For macOS
brew tap heroku/brew && brew install heroku

# For Windows (using scoop)
scoop install heroku

# For Linux
curl https://cli-assets.heroku.com/install.sh | sh
```

3. Login to Heroku:
```bash
heroku login
```

4. Create a new Heroku app:
```bash
heroku create practical-test-api
```

5. Add the .NET buildpack:
```bash
heroku buildpacks:set heroku/dotnet
```

6. Create a free PostgreSQL database (temporário para desenvolvimento):
```bash
heroku addons:create heroku-postgresql:hobby-dev
```

7. Configure environment variables:
```bash
heroku config:set ASPNETCORE_ENVIRONMENT=Development
```

8. Deploy the application:
```bash
git push heroku main
```

9. Open the application:
```bash
heroku open
```

## Running the Application

### Local Development
1. Open a terminal in the solution directory
2. Run the following commands:

```bash
cd PracticalTest.Api
dotnet run
```

The API will be available at `https://localhost:7001` and `http://localhost:5001`

### Heroku Development
The API will be available at `https://your-app-name.herokuapp.com`

## API Endpoints

- GET /api/transactions - Get all transactions
- GET /api/transactions/{id} - Get a transaction by ID
- POST /api/transactions - Create a new transaction
- PUT /api/transactions/{id} - Update a transaction
- DELETE /api/transactions/{id} - Delete a transaction

## Running Tests

To run the tests, use the following command:

```bash
dotnet test
```

## Project Structure

- `PracticalTest.Api` - Main API project
  - `Controllers` - API endpoints
  - `Models` - Domain models
  - `Data` - Data access layer
  - `CQRS` - Commands and queries
- `PracticalTest.Tests` - Unit tests

## Technologies Used

- .NET Core 7.0
- Dapper
- MediatR (CQRS)
- Oracle Database
- xUnit (Testing)
- Moq (Mocking)
- Swagger/OpenAPI

## Troubleshooting

### Docker Issues
- If you get permission errors when pulling the Oracle image, you may need to login to Oracle Container Registry:
```bash
docker login container-registry.oracle.com
```
- If the container fails to start, check the logs:
```bash
docker-compose logs oracle
```

### Database Connection Issues
- Make sure the Oracle container is running:
```bash
docker-compose ps
```
- Check if you can connect to the database using SQL*Plus or another Oracle client
- Verify the connection string in `appsettings.json` matches your setup

### Heroku Issues
- Check the logs:
```bash
heroku logs --tail
```
- Verify environment variables:
```bash
heroku config
```
- Restart the application if needed:
```bash
heroku restart
```
