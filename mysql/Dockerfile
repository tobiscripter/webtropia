# syntax = docker/dockerfile:experimental
FROM alpine

RUN apk add --no-cache mariadb mariadb-client mariadb-server-utils pwgen && rm -f /var/cache/apk/*

RUN mkdir /docker-entrypoint-initdb.d && mkdir /scripts/pre-exec.d && mkdir /scripts/pre-init.d && chmod -R 755 /scripts

ADD run.sh /scripts/run.sh

EXPOSE 3306

RUN mkdir /secrets
RUN --mount=type=secret,id=MYSQL_ROOT_PASSWORD \
    export MYSQL_ROOT_PASSWORD="$(cat /run/secrets/MYSQL_ROOT_PASSWORD)"


ENTRYPOINT ["/scripts/run.sh"]