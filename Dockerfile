FROM docker.io/library/alpine:edge AS builder

RUN apk add --no-cache dotnet7-sdk

WORKDIR /src

COPY . .

RUN sed -i "s:net6.0:net7.0:g" ANXlytics.csproj && \
    dotnet publish \
        -c Release \
        -r linux-musl-x64 \
        --self-contained \
        -p:PublishReadyToRun=true \
        -p:PublishTrimmed=true \
        -p:PublishSingleFile=true \
        -p:CrossGenDuringPublish=false \
    && \
    cp bin/Release/net7.0/linux-musl-x64/publish/ANXlytics /ANXlytics && \
    cp bin/Release/net7.0/linux-musl-x64/publish/libe_sqlite3.so /libe_sqlite3.so

FROM docker.io/library/alpine:edge

RUN apk add --no-cache libgcc libstdc++ icu-libs

WORKDIR /app

COPY appsettings.container.json appsettings.json
COPY --from=builder /libe_sqlite3.so /app/libe_sqlite3.so
COPY --from=builder /ANXlytics /app/ANXlytics

CMD ["/app/ANXlytics"]
