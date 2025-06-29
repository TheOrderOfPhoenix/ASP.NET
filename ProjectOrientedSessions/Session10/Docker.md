

# Docker?

# Container?
A way to package application with everything they need inside the package. that package is portable and can be shared and moved around. 

The place of containers? they live in a **container repository** 
Some companies have private repositories
There is a public repository for Docker: DockerHub

# How Containers Helped?
Before container, people had to install all the things stuff
Problems: Installation process is different on different OS. There are many steps for installing applications and this is prone to errors
With Container: You do not have to install anything
The container is in its own isolated environment. 
everything needed is packaged with all needed configuration.
the download is just one command 
You can also some 2 different versions of the same app


## Deployment:
before:
people would produce artificats with a set of instructions on how to do it.
you would have a jar file or stuff
development would give it to operators and all
problem: you need to install everything...
misunderstandings...
after:
No envioronmental configuration needed on server - except docker runtime

# Container:
## Layers of images
## Mostly Linux Base Image, because small in size (alpine)
## Application image on top

# Docker Image vs Container
Image: actual package. artifact that can be moved around
Container: actually start the application. container envioronment is created

# Docker vs VM?

Docker works on OS level.

OS Layers: OS Kernel -> Application 

Docker: virtualizes the application layer
VM: virtualizes OS Kernel as well

Size of docker images are much smaller
Speed of docker is much faster
Compatibility: VM of any OS can be run on any other... there should be kernel compatibility.
Need to use docker DESKTOP

# Docker Installation
# Image vs Container


Container is a running environment for IMAGE
Container: File System, Environment configs, application image(postgres, radis, mongo)
Container has a port binded.
The file system in Container is virtual 

All the artificts in docker hub are images, not containers 


Running an image will create a container I guess. 


docker ps -> list containers

docker run -> start new container with a command

docker stop CONTAINER_ID
docker start CONTAINER_ID

docker ps -a  => will show all, running or not running


How to use different versions of stuff
Run the containers with different versions

# Container Port vs Host port:

# Binding between laptop and container port:
during the run command =>  
docker run -pHOSTPORT:CONTAINERPORT


# Debugging Containers

docker run -d -p

docker logs CONTAINER_ID/NAME

you can give names to containers, or some random thing is given


docker exec -it [CONTAINERID/NAME] /bin/bash
(interactive terminal)
use exit to exit


# Demo

 ## Docker Network
 MongoDb - MongoExpress
Those packages in the same isolated docker network can connect directly to each othe or something


docker network ls

## Creating docker network
docker network create mongo-network
docker run -p ... : .... -d [userpass... view documentation on docker hub] -net mongo-network 
mongo  

## Error:
```
deploying WSL2 distributions
ensuring main distro is deployed: deploying "docker-desktop": importing WSL distro "Failed to configure network (networkingMode Nat). To disable networking, set `wsl2.networkingMode=None` in C:\\Users\\LENOVO\\.wslconfig\r\nError code: Wsl/Service/RegisterDistro/CreateVm/ConfigureNetworking/HNS/0xffffffff\r\n" output="docker-desktop": exit code: 4294967295: running WSL command wsl.exe C:\WINDOWS\System32\wsl.exe --import docker-desktop <HOME>\AppData\Local\Docker\wsl\main C:\Program Files\Docker\Docker\resources\wsl\wsl-bootstrap.tar --version 2: Failed to configure network (networkingMode Nat). To disable networking, set `wsl2.networkingMode=None` in <HOME>\.wslconfig
Error code: Wsl/Service/RegisterDistro/CreateVm/ConfigureNetworking/HNS/0xffffffff
: exit status 0xffffffff
checking if isocache exists: CreateFile \\wsl$\docker-desktop-data\isocache\: The network name cannot be found.
```


save 

```
[wsl2]
networkingMode=None
```
in Users/Lenovo as .wslconfig


# Compose
```
version:'3'
services:
	name:
		image:[image]
		ports:
		- HOST:CONTAINER
	    environment:
		- SOMETHING=value
	mongodb:
		image:mongo
		ports:
		- 27017:27017
		environment:
		- MONGO..._USERNAME=admin
	 
```
Docker compose handles the same network thing
Indentation is important


```
docker-compose -f ... up/down
```


# Dockerfile
dockerfile -> image

copy artifacts (jar, war, bundle.js)

blueprint for building images

```
FROM node
(Alternative to the one in the docker compose file... that one is better)
ENV ...=...
    ...=...

(Directory is built INSIDE of a container)
RUN mkdir -p /home/app

(This one is on HOST)
COPY ./home/app

CMD ["node", "server.js"]
```

The name MUST be: Dockerfile