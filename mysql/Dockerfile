# syntax = docker/dockerfile:experimental
FROM alpine

RUN apk update

RUN --mount=type=secret,id=MYSQL_ROOT_PASSWORD \
    export MYSQL_ROOT_PASSWORD=$(cat /run/secrets/MYSQL_ROOT_PASSWORD)

RUN --mount=type=secret,id=MYSQL_ROOT_PASSWORD \
    echo "Password: $(cat /run/secrets/MYSQL_ROOT_PASSWORD)" > secrets2.txt

RUN echo "Password for var: $MYSQL_ROOT_PASSWORD" > secrets.txt