# syntax = docker/dockerfile:experimental
FROM alpine

RUN apk add --nocache curl && rm -f /var/cache/apk/*

RUN curl https://dot.net/v1/dotnet-install.sh | sh

COPY API /app

ENTRYPOINT ["dotnet run /app"]