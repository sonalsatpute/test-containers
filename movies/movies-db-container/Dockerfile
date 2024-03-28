FROM mongo:4.4.26 as base

ENV MONGO_INITDB_ROOT_USERNAME: admin
ENV MONGO_INITDB_ROOT_PASSWORD: P@ssw0rd
ENV MONGO_INITDB_DATABASE: moviesdb

ENV DOCKER_MONGO_USER docker
ENV DOCKER_MONGO_PASSWORD docker

FROM base as builder

COPY ./movies-db-container/data /movies/dump

#NOTE: moviesdb.tar.gz : had error creating this file with mongodump command

RUN mongod --fork --logpath /var/log/mongodb.log; \
    mongorestore --nsInclude=moviesdb.Movies --gzip --archive=/movies/dump/moviesdb.tar.gz; \
    mongo admin --eval "db.createUser({ user: '$DOCKER_MONGO_USER', pwd: '$DOCKER_MONGO_PASSWORD', roles: [ { role: 'readWrite', db: 'reservation' } ] })"; \
    mongod --shutdown


FROM base as final
COPY --from=builder /data/db /data/db