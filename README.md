# General goal

To setup an example of using [Consul](https://www.consul.io) as a service discovery solution for a micro service architecture.

## Demo I

The first step is to setup a _Consul_ server and a _Consul_ agent having two services that register and then talk to each other.

### Summary architecture

#### Host
* Consul server: server to coordinates the agents

#### Client
* Consul agent: agent that the services use to connect to the server
* Service1: Service that provides information
* Service2: Service that consumes service 1

### Demo

* Start consul server on host
    * Check UI to see one node
* Start consul agent on host
    * Check UI to see both nodes
* Start service 1 on client
    * Check localhost:8500
* Start service 2

