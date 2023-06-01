# Entities Map

This is an exercise repo for Aeronautics.
The project is a demo consisting of 2 microservices communicating with each other with rabbitmq.
The Creator service publish coordinate events to a fanout exchange.
The Presenter service subscribes to the fanout exchange and consumes the events showing the coordinates on a map.

## Running the project:

```bash
docker-compose create
docker-compose up -d
```
