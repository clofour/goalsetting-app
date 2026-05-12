# Update directories once repository structure is finalized
FROM dhi.io/dotnet:10.0.203-sdk-debian13 AS build
WORKDIR /src
COPY backend.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app

FROM dhi.io/aspnetcore:10.0.7-debian13 AS run
COPY --from=build /app .
ENTRYPOINT ["dotnet", "backend.dll"]