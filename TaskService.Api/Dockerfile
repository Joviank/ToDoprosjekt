# ---------------BUILD---------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .

WORKDIR /src/TaskService.Api
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish
# /app/publish/TaskService.Api.dll

# -------------RUNTIME---------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "TaskService.Api.dll" ]
