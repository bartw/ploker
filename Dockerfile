FROM microsoft/aspnetcore
ENV PORT="5001"
WORKDIR /app
COPY . .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Ploker.Server.dll