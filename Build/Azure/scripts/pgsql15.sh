#!/bin/bash

# TODO: update tag to 15 when it is released
docker run -d --name pgsql -e POSTGRES_PASSWORD=Password12! -p 5432:5432 -v /var/run/postgresql:/var/run/postgresql postgres:15rc1

retries=0
until docker exec pgsql psql -U postgres -c '\l' | grep -q 'testdata'; do
    sleep 1
    retries=`expr $retries + 1`
    docker exec pgsql psql -U postgres -c 'create database testdata'
    if [ $retries -gt 100 ]; then
        echo postgres not started or database failed to create
        exit 1
    fi;
done

docker logs pgsql
