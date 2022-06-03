FROM alpine

RUN apk update

RUN --mount=type=secret,id=MYSQL_ROOT_PASSWORD \
   export MYSQL_ROOT_PASSWORD=$(cat /run/secrets/MYSQL_ROOT_PASSWORD)

RUN echo "$MYSQL_ROOT_PASSWORD" > secrets.txt