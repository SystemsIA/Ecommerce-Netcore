FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

ENV DOTNET_EnableDiagnostics=0
ENV DATABASE_SOURCE_APP $DATABASE_SOURCE_APP

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY App/App.csproj App/
COPY Domain/Domain.csproj App/

RUN dotnet restore "App/App.csproj" && dotnet restore "App/Domain.csproj"
# RUN dotnet user-secrets init --project /src/App/App.csproj
# RUN dotnet user-secrets set ConnectionStrings:ConnectionDb "$DATABASE_SOURCE_APP" --project /src/App/App.csproj

COPY . .

# App
WORKDIR "/src/App"
RUN dotnet build "App.csproj" -c Release -o /app/build_app
FROM build AS publish_app
RUN dotnet publish "App.csproj" -c Release -o /app/publish_app


FROM base AS final
ENV ASPNETCORE_URLS http://*:$PORT
WORKDIR /app
COPY --from=publish_app /app/publish_app .
# ENTRYPOINT ["dotnet", "App.dll"]
CMD dotnet App.dll
