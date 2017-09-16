FROM microsoft/aspnetcore
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "Ploker.Server.dll"]