
## What is Docker?

Docker is a platform that allows you to package applications with all their dependencies into a standardized unit called a **container**. These containers are portable, isolated, and consistent across environments.

---

## What is a Container?

A container is a lightweight, standalone executable package that includes everything needed to run an application: code, runtime, libraries, and configurations.

Containers are stored in **container repositories**:
- Public repositories: [Docker Hub](https://hub.docker.com)
- Private repositories: Used by organizations for internal deployments

---

## Why Containers?

### Before Containers:
- Developers shared artifacts (e.g., `.jar` files) with setup instructions.
- Operators had to install dependencies manually.
- Setup was error-prone and inconsistent across OS environments.

### With Containers:
- Everything is bundled together and works the same everywhere.
- No need to install dependencies manually.
- Runs in its own isolated environment.
- Easy to version, share, and deploy (just one command).
- Multiple versions of the same app can run simultaneously.

---

## Image vs. Container

|Term|Description|
|---|---|
|**Image**|A snapshot or package (the blueprint). Immutable.|
|**Container**|A running instance of an image. Has its own file system, environment, and process.|
Running an image creates a container.

---

## Docker vs Virtual Machine

| Feature         | Docker             | Virtual Machine    |
| --------------- | ------------------ | ------------------ |
| Virtualizes     | Application layer  | Full OS kernel     |
| Startup time    | Seconds            | Minutes            |
| Size            | MBs                | GBs                |
| Isolation       | OS-level           | Hardware-level     |
| Performance     | Near-native        | Heavier overhead   |
| Host dependency | Shares host kernel | Has its own kernel |

Docker runs natively on Linux; on Windows/macOS it uses **Docker Desktop**, which runs Linux under WSL2 or HyperKit.

---

## Docker Architecture

### Layers of a Docker Image:

- Base Layer: Usually a minimal Linux distribution (e.g., `alpine`)
- Application Layer: Your app and its dependencies

Each image is made of **layers** stacked on top of each other.

---

## Docker Installation

To use Docker on:

- **Linux**: Install Docker engine directly.
- **Windows/macOS**: Use Docker Desktop, which includes WSL2 integration or virtualization backend.

---

## Docker Commands: Basics

```bash
# Run a container from an image
docker run image-name

# List running containers
docker ps

# List all containers (running and stopped)
docker ps -a

# Stop a running container
docker stop CONTAINER_ID

# Start a stopped container
docker start CONTAINER_ID
```

### Port Binding

```bash
docker run -p HOST_PORT:CONTAINER_PORT image-name
```

This binds a container’s port to a specific port on your machine.

---

## Debugging Containers

```bash
# View logs
docker logs CONTAINER_ID

# Start a container with detached mode and port
docker run -d -p 3000:3000 image-name

# Open an interactive shell inside a running container
docker exec -it CONTAINER_ID /bin/bash
```

You can assign names to containers using `--name`.

---

## Docker Networking

### Concept:

Containers can communicate with each other over a virtual network.

```bash
# List networks
docker network ls

# Create a new network
docker network create my-network

# Run container in a network
docker run --net my-network ...
```

Example: MongoDB + Mongo Express on same network can communicate via service name.

---

## Docker WSL2 Error (Windows)

### Issue:

```
Failed to configure network (networkingMode Nat)...
```

### Fix:

Create or edit the file at:

```
C:\Users\LENOVO\.wslconfig
```

Add:

```
[wsl2]
networkingMode=None
```

Then restart Docker Desktop.

---

## Docker Compose

Docker Compose lets you define and run multi-container apps using YAML.

### Example:

```yaml
version: '3'
services:
  app:
    image: my-app
    ports:
      - "3000:3000"
    environment:
      - NODE_ENV=production

  mongodb:
    image: mongo
    ports:
      - "27017:27017"
    environment:
      - MONGO_INITDB_ROOT_USERNAME=admin
      - MONGO_INITDB_ROOT_PASSWORD=secret
```

### Commands:

```bash
docker-compose -f docker-compose.yml up
docker-compose down
```

- All services run in the same default Docker network.
- Indentation in YAML is **critical**.

---

## `Dockerfile`

A `Dockerfile` is a script used to build Docker images.

### Example:

```Dockerfile
FROM node:18

ENV NODE_ENV=production

# Inside container
RUN mkdir -p /home/app

# Copy from host into container
COPY ./home/app /home/app

WORKDIR /home/app

CMD ["node", "server.js"]
```

Build with:

```bash
docker build -t my-node-app .
```

Then run it with:

```bash
docker run -p 3000:3000 my-node-app
```

