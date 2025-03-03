FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY RecipeApp.sln .
COPY RecipeApp.API/RecipeApp.API.csproj RecipeApp.API/
COPY RecipeApp.Application/RecipeApp.Application.csproj RecipeApp.Application/
COPY RecipeApp.Domain/RecipeApp.Domain.csproj RecipeApp.Domain/
COPY RecipeApp.Infrastructure/RecipeApp.Infrastructure.csproj RecipeApp.Infrastructure/

RUN dotnet restore

COPY RecipeApp.API/ RecipeApp.API/
COPY RecipeApp.Application/ RecipeApp.Application/
COPY RecipeApp.Domain/ RecipeApp.Domain/
COPY RecipeApp.Infrastructure/ RecipeApp.Infrastructure/

WORKDIR /src/RecipeApp.API
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RecipeApp.API.dll"]