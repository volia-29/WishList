FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore && dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS prod
WORKDIR /app
COPY --from=build /src/publish .
EXPOSE 5227
ENTRYPOINT ["dotnet", "WishList.Razor.App.dll"]
