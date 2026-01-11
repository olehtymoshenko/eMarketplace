#!/bin/bash

SERVICES_TO_RUN="catalog-api-db seq keycloak-db keycloak"

echo "Starting services: $SERVICES_TO_RUN"

docker compose up -d $SERVICES_TO_RUN
