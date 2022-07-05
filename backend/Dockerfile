# syntax = docker/dockerfile:experimental
FROM alpine

RUN apk add --no-cache bash icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib curl
RUN apk add libgdiplus --repository https://dl-3.alpinelinux.org/alpine/edge/testing/
RUN rm -f /var/cache/apk/*

RUN wget https://dot.net/v1/dotnet-install.sh

RUN chmod +x dotnet-install.sh
RUN ./dotnet-install.sh -c Current --verbose 
#--runtime aspnetcore
ENV PATH $PATH:/root/.dotnet/tools
ENV PATH $PATH:/root/.dotnet
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1
RUN dotnet --version

COPY backend/API /app

EXPOSE 2000

RUN dotnet dev-certs https --trust
RUN dotnet build /app

ENTRYPOINT ["dotnet", "run", "--project", "app"]