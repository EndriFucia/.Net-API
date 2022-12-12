#Compile project
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build_env
WORKDIR /App
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o output

#Create runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
EXPOSE 80
COPY --from=build_env /App/output .
COPY database.env .
ENTRYPOINT ["dotnet", ".Net-API.dll"]