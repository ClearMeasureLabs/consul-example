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

## Demo II

Setup _Consul_ with _Nginx_ so when the services register to consul and that triggers a change in _Nginx_ configuration.

Also the services will use _Consul_ to connect to _Nginx_ and use it as reverse proxy and load balancer.

### Summary architecture

### Host
* Consul server: server to coordinate the agents
* [Consul-template](https://hashicorp.com/blog/introducing-consul-template.html): daemon to write and update _Nginx_ configuration
* [Nginx](https://www.nginx.com/): Reverse proxy and load balancer
 

## Demo III

Same idea as _Demo I_ but using _Docker_ to run consul server and agent.

## Host

Run command

`docker run --name consul_server -p 8400:8400 -p 8500:8500 -p 8600:53/udp -h server1 progrium/consul -server -bootstrap`
	
### Mac OS
Forward ports on the default VM (8400, 8301, 8500 8600) TCP and UDP
	
	
## Windows Client

Create configuration `agent.json` in folder `config` with:

> {
>        "client_addr": "0.0.0.0",
>        "data_dir": "c:/users/admin/consul",
>        "leave_on_terminate": true,
>        "dns_config": {
>                "allow_stale": true,
>                "max_stale": "1s"
>        }
>}

Run

`consul.exe agent -advertise 127.0.0.1 -config-dir=config -log-level=DEBUG -node=agent1`

Run join to join the agent to the cluster

`consul.exe join -rpc-addr HOST_IP:8400 AGENT_IP`

To see the members in the cluster

`consul.exe members -rpc-addr HOST_IP:8400`

> Node     Address          Status  Type    Build  Protocol  DC
> agent1   127.0.0.1:8301   alive   client  0.6.1  2         dc1
> server1  172.17.0.2:8301  alive   server  0.5.2  2         dc1

### Agent commands

Using a browser to http://localhost:8500

* Deregister a service: /v1/agent/service/deregister/YORSERVICE_ID