MicroserviceProject

This project demonstrates a simple microservice architecture using:

.NET 8

Amazon SQS

Docker and Docker Compose

Communication via queue (pub/sub)

Structure

MicroserviceProject/
??? OrderService/         # Web API that publishes messages to SQS
?   ??? Program.cs
?   ??? Services/
?   ??? Controllers/
?   ??? Dockerfile
??? OrderProcessor/       # Console App that consumes messages from SQS
?   ??? Program.cs
?   ??? Dockerfile
??? docker-compose.yml
??? MicroserviceProject.sln

Requirements

Docker Desktop

AWS account with SQS access

Visual Studio 2022+ or .NET SDK 8+

Configuration

In the appsettings.json of both projects:

"AWS": {
  "AccessKey": "YOUR_ACCESS_KEY",
  "SecretKey": "YOUR_SECRET_KEY",
  "Region": "us-east-2",
  "SqsQueueUrl": "https://sqs.us-east-2.amazonaws.com/YOUR_ID/MicroserviceBasicApi"
}

Running with Docker Compose

In the root folder, run:

docker-compose up --build

order-service will be exposed at: http://localhost:7140

order-processor will listen for messages from the queue

Test with Postman

Endpoint:

POST http://localhost:7140/api/Order/create-order

JSON Body:

{
  "product": "notebook",
  "quantity": 1,
  "price": 2500
}

Expected:

order-service publishes the message to SQS

order-processor receives the message and logs:

Listening for messages...

Notes

OrderService listens on port 80 inside the container, mapped to port 7140 on the host

Ensure your OrderService Program.cs includes:

builder.WebHost.UseUrls("http://+:80");

Services communicate indirectly via SQS (no direct HTTP calls between them)

Built by Caetano Petropolis.