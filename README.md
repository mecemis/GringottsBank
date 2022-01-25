# GringottsBank

## IdentityServer
Identityserver4 implementation has been made to perform user operations in the system.
JWT infrastructure is used for user authorizations 

## BankAccount Service
### Web Api
It is the client part that enables the execution of banking transactions. 

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. 

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

## Build and Test
### Docker Container
    docker-compose up
    
### Postman Collection
    GringottsCollection.postman_collection.json
