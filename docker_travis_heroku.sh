cp ./Dockerfile ./dist
cd dist
docker build -t bartw/ploker:$TRAVIS_BUILD_NUMBER .
docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push bartw/ploker:$TRAVIS_BUILD_NUMBER
docker login -u=$HEROKU_USERNAME -p=$HEROKU_AUTH_TOKEN registry.heroku.com;
docker tag bartw/ploker:$TRAVIS_BUILD_NUMBER registry.heroku.com/ploker/web;
docker push registry.heroku.com/ploker/web;