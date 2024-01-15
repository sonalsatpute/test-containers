## Configure remote docker host:


Test Containers need docker host to run and docker build process runs in issolated environment where we don’t have docker host locally available.
One of the solution to fix this issue to use remote docker host. All we have to do is enable the tcp port of docker host and update the .testcontainer.properties file with IP of the host.

Expose docker for remote connection


First, create a file named daemon.json under /etc/docker (edit if that file is already available)

```json
{
"hosts": ["tcp://0.0.0.0:2375", "unix:///var/run/docker.sock"]
}
```
Next, create `docker.service.d` folder


```shell
sudo mkdir -p /etc/systemd/system/docker.service.d/
```
Then create this file: /etc/systemd/system/docker.service.d/override.conf

```shell
[Service]
ExecStart=
ExecStart=/usr/bin/dockerd
```

Now, execute the following commands with root privileges

```shell
sudo systemctl daemon-reload
sudo systemctl restart docker
```
### Configure TestContainers to connect to Remote Docker enviroment
TestContainers has detailed configuration tutorials here, find out where is the configuration file on your system.

On Windows, that would be `C:\Users\<your user name>\`

Create and edit a file at that location named `.testcontainers.properties` and put the following content


```shell
docker.client.strategy=org.testcontainers.dockerclient.EnvironmentAndSystemPropertyClientProviderStrategy
docker.host=tcp://192.168.0.9:2375
```

You are all set now! if you run your tests now your testcontainer will be created on remote host.

To Run your integration test(s) as party of your build process
copy the `.testcontainers.properties` to your project

and add the below line to your dockerfile

```dockerfile
COPY ./.testcontainers.properties /root/.testcontainers.properties
```

build your docker file

```shell
docker build -t <your-image-name> .
```