# Library WCF Service

Library WCF Service is a .NET Framework WCF project that exposes a small library record system through service operations. The project includes a hosted WCF service and a console client that connects to the service, allowing users to add, update, delete, search, check out, return, and check availability for books.

## What It Does

This project demonstrates how a WCF service defines operations, exposes a data contract, handles service-side validation, and is consumed by a separate client application.

The service manages an in-memory collection of books. Each book has an ID, title, author, ISBN, and availability status. The console client provides a menu-driven interface for calling the service operations and handling service errors.

## Tech Stack

* C#
* .NET Framework 4.8
* Windows Communication Foundation (WCF)
* Visual Studio
* IIS Express
* Service contracts
* Data contracts
* Operation contracts
* WCF client proxy / connected service
* Console application client

## Features

* WCF service contract defining available service operations
* `Book` data contract with serializable book properties
* Add, update, delete, and retrieve book records
* List all books
* Search books by title, author, or ISBN
* Check book availability
* Check out and return books
* Service-side validation using `FaultException`
* Console client that consumes the WCF service through a generated service reference
* WCF configuration for local development with IIS Express

## What I Learned

This project showed me that building a WCF service involves more than writing methods. It requires a defined contract, serializable data models, proper configuration, and a client that can communicate with the service.

I also learned how service boundaries affect application structure. The console client interacts with the service through a generated proxy instead of managing data directly, which clarified the separation between service provider and consumer.

It also gave me experience with handling errors across a service boundary, where the service returns faults that the client must handle safely.

## Development Process / AI Use

I built this project from course examples and textbook patterns, creating the service contract, data contract, implementation, configuration, and console client. I then expanded it into a simple library system with book management, searching, checkout, return, and availability features.

AI was used as a learning aid to reinforce WCF concepts, review structure, and clarify configuration or client-service communication when needed. I still relied on course material, reviewed the generated code, and tested everything through the console client.

A limitation is that the data is stored in memory, so it does not persist after the service stops. The focus of the project was learning how a WCF service is defined, hosted, configured, and consumed.
