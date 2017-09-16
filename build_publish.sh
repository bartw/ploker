rm -rf ./dist
cd Ploker.Client
npm install
npm run build
cd ..
dotnet publish ./Ploker.Server -c Release -o ../dist