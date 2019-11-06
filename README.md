
# Building docker image 

Being at src directory create the docker image.

```
docker build -t kafka-subscriber .
```

# Running docker containers

## Without volumes

No output data will be shared at your local file system.

```
docker run --name sub1 --network=docker_kafka kafka-subscriber -g g0 -t samples1 -b broker:29092 -o /data
```

## With volumes

Output data will be shared at d:/tests/output.

```
docker run --name sub1 --network=docker_kafka -v d:/tests/output:/data kafka-subscriber -g g0 -t samples1 -b broker:29092 -o /data
```