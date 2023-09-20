FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0.11-alpine3.18
EXPOSE 5002

ENV ASPNETCORE_URLS=http://+:5002
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Server.dll"]