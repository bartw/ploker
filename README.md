[![Build Status](https://travis-ci.org/bartw/ploker.svg)](https://travis-ci.org/bartw/ploker)

# Ploker

## Prerequisites

- [Git](https://git-scm.com/)
- [NodeJs](https://nodejs.org/en/)
- [.NET Core](https://www.microsoft.com/net/core)
- [docker](https://www.docker.com/)

## Build, publish and run on a local docker

```shell
git clone https://github.com/bartw/ploker
cd ploker
sh install.sh
sh build_publish.sh
sh docker.sh
docker run -d -p 8000:80 ploker
```

## License

Ploker is licensed under the [MIT License](LICENSE).