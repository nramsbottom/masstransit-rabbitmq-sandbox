
MassTransit with RabbitMQ Sandbox
=================================

Just my experimenting with queues. Requires a RabbitMQ running on localhost to function, which you can run very easily with Docker.

   docker run -p 15672:15672 -p 5672:5672 --name rabbitmq -d rabbitmq:3.8.0-management