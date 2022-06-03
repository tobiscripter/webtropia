# syntax = docker/dockerfile:experimental
FROM alpine

RUN apk update

RUN --mount=type=secret,id=MYSQL_ROOT_PASSWORD
RUN export MYSQL_ROOT_PASSWORD=$(cat /run/secrets/MYSQL_ROOT_PASSWORD)

RUN echo "Password: $MYSQL_ROOT_PASSWORD" > secrets.txt