# syntax = docker/dockerfile:experimental
FROM alpine

RUN apk update

RUN --mount=type=secret,id=MYSQL_ROOT_PASSWORD \
    export MYSQL_ROOT_PASSWORD=$(cat /run/secrets/MYSQL_ROOT_PASSWORD)

RUN mkdir /secrets

RUN --mount=type=secret,id=MYSQL_ROOT_PASSWORD \
    echo "Password: $(cat /run/secrets/MYSQL_ROOT_PASSWORD)" > /secrets/MYSQL_ROOT_PASSWORD

