FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5002

ENV ASPNETCORE_URLS=http://+:5002

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["src/Server/Server.csproj", "src/Server/"]
RUN dotnet restore "src/Server/Server.csproj"
COPY . .

FROM build AS publish
WORKDIR "/src/WebAssembly/Server"
RUN dotnet publish "Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.dll"]
HEALTHCHECK CMD curl --fail http://localhost:5002/health || exit
