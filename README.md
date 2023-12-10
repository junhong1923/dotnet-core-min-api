# About this dotnet-core-min-api project

## Requirements
- Language: C#
- Framework: dotnet 7.0
- Datebase: Postgres 15.4

## SPEC
- **Query API** : http://{IP}/query?sno=1234
- **Fake API** : http://{IP}/fake?num=2
    - Limit: 1 <= num <= 100
- **Repor API** : http://{IP}/report
    - Currently consoling output only

## How to run this project?
1. Install .NET SDK 7.0
2. Git pull this project
3. **Update `appsettings.json` file with connection secrets**
4. Build code under the project directory
with this command: `$ dotnet build`
5. Run project under the project directory
with this command: `$ dotnet run`

## TODO
- Store report on AWS S3
- Cache query api result
- Run dotnet application in the background so that it would not be terminated due to disconnectining to the session
- Maybe storing log output physically into files