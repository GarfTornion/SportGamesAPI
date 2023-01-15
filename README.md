# SportGamesAPI

A simple ASP.NET app that creates an API-interface to manage the scoring of sport games involving two teams. Originally designed to run on Azure Cloud -services.

The repository includes a Client app as a submodule. A separate repo for the client can be found [here](https://github.com/GarfTornion/SportGamesWebUI).

A live demo can be found [here](https://sportgamesapi.azurewebsites.net/swagger/index.html) along with API-usage documentation.

## Setup

Copy the repo to your system. You will need to setup a database for storing the sport games.

You can initialise the required database table(s) with the [init_db.sql](init_db.sql) file found within the repo.

To establish a database connection in a local environment, you must define the connection string for the database with the following commands in the project:

```
dotnet user-secrets init
dotnet user-secrets set ConnectionStrings:SportGameDb "<your-connection-string>"
```

Once done, you should be able to run the application. By default the app will run at `https://localhost:7175/`