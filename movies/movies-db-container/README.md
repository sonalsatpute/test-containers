# moviesdb(mongo-db) container
 

### build

```shell
docker build --no-cache --progress=plain -t mongodb:movies -f ./movies-db-container/Dockerfile .
```

### start moviesdb container
```shell
docker run -it --rm -p 27017:27017 --name mongo-db-reservation mongodb:movies
```

### dump reservation database

```shell
docker run mongo:4.4.26 /usr/bin/mongodump  --host <host name/ip> --port <port-number> --username <user-name> --password <password> --authenticationDatabase admin --db <database-name> --collection <collection-name>  --archive > <dump-file-name>.tar.gz
```
> NOTE: moviesdb.tar.gz : had error creating this file with mongodump command
```shell
docker run mongo:4.4.26 /usr/bin/mongodump  --host localhost --port 27017 --username sonal --password mypassword1! --authenticationDatabase admin --db movies --collection Movies  --archive > moviesdb.tar.gz
```