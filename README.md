# RabbitMQConsumerOnBlazorServer
A small example of how a blazor server can receive messages(in the form of products) from RabbitMQ and refresh a page with the newest products, whenever a new one is sent through the queue. This is just a sample project made to test how blazor can display changes in data, made by an external modifier (namely a rabbitmq producer publishing a message).
This could be easily mdified to work with any type of message sent through a queue. I, for one, used a spring boot producer that puts objects of type product into the queue. 
This is one of my repositories: https://github.com/Mishanull/rabbitmq-in-springboot.git
