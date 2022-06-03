# syntax = docker/dockerfile:experimental
FROM alpine

RUN apk add --no-cache mariadb mariadb-client mariadb-server-utils pwgen && rm -f /var/cache/apk/*

RUN mkdir -p /docker-entrypoint-initdb.d
RUN mkdir -p /scripts/pre-exec.d 
RUN mkdir -p /scripts/pre-init.d 

ADD mysql/run.sh /scripts/run.sh

RUN chmod -R 755 /scripts


EXPOSE 3306

RUN mkdir /secrets
RUN --mount=type=secret,id=MYSQL_ROOT_PASSWORD \
    export MYSQL_ROOT_PASSWORD="$(cat /run/secrets/MYSQL_ROOT_PASSWORD)"


ENTRYPOINT ["/scripts/run.sh"]