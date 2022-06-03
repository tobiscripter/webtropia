# syntax = docker/dockerfile:experimental
FROM alpine

RUN apk add --no-cache mariadb mariadb-client mariadb-server-utils pwgen && rm -f /var/cache/apk/*

RUN mkdir -p /docker-entrypoint-initdb.d
RUN mkdir -p /scripts/pre-exec.d 
RUN mkdir -p /scripts/pre-init.d 

ADD mysql/run.sh /scripts/run.sh

RUN chmod -R 755 /scripts
RUN chmod -R 755 /etc/init.d/

EXPOSE 3306

RUN --mount=type=secret,id=MYSQL_ROOT_PASSWORD \
    echo "MYSQL_ROOT_PASSWORD=$(cat /run/secrets/MYSQL_ROOT_PASSWORD)" >> /secrets/mysql_root_password


ENTRYPOINT ["/scripts/run.sh"]