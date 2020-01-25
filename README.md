# TodoWebApi

In case server to work in proper way, MongoDb must be running and connection string in appsettings.json (appsettings.Development.json) must be specified.

For example MongoDb may be inside of docker container:
```
docker run -p 27017:27017 mongo
```
And your appsettings.Development.json DatabaseSettings seciton may look like:
```
 "DatabaseSettings": {
    "ConnectionString" : "mongodb://localhost:27017",
    "TodoDatabase" : "TodoDB",
    "TopicCollection" : "Topics",
    "TaskCollection" : "Tasks"
  }
```
