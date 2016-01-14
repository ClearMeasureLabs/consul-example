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


### Demo steps

#### Host

1. Install consul and then run

    `consul agent -server -bootstrap -data-dir /tmp/consul -node server1`

#### Agent

1. Install consul and run

    `consul agent -host agent -data-dir YOUR_DATA_DIR`

2. Check members (only one should appear)

    `consul members`

Something like this should appear:

    Node     Address            Status  Type    Build  Protocol  DC
    agent    192.168.3.39:8301  alive   client  0.6.1  2         dc1

3. Join both agent and server

    `consul join HOST_IP AGENT_IP`

4. Check members (both of them should appear)

    `consul members`

Something like this should appear:

    Node     Address            Status  Type    Build  Protocol  DC
    agent    192.168.3.39:8301  alive   client  0.6.1  2         dc1
    server1  192.168.3.31:8301  alive   server  0.5.2  2         dc1

### Testing the services

Open a browser and check the services on the agent:

    http://localhost:8500/v1/agent/services

Run `build.cmd demo` to launch one instance of each service.

With both server an agent running lets check the services configuration again

    http://localhost:8500/v1/agent/services

Or to check one service

    http://localhost:8500/v1/health/service/ConsulDemoService1API

Here you can see the port each service is running, using that info ping each service:

    http://localhost:SERVICE1_IP/ping

    http://localhost:SERVICE2_IP/ping

Now let's check the `customers` on service 2

    http://localhost:SERVICE2_IP/customers

This service is using _Consul_ to get the service 1 information and call `ping`. 
The browser should show something like this:

    Service 1 at http://YOUR_HOST_NAME:SERVICE1_PORT returns OK with "Service1 PING! 14/01/2016 9:12:13 AM"