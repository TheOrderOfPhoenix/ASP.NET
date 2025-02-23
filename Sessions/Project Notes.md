# Name: alibaba.ir

## Stack
> .NET 8 (LTS) - Domain Driven Design (DDD)
> Database: MS SQL Server - Code First - EF Core
> Front: React with TS

## Entities

### Person
id
firstname
lastname
...
### Role
Id
Title (User, Admin, Emp)
### Account
PersonId
RoleId
password

### Company
id
title

### Transportation
id
srcId
destId
startTime
endTime
capacity
remainingPlaces
vehicleId
price
companyId

### City
id
title

### Base
id
location
type
cityId

### Vehicle
id
typeId

### Company
### Type

### Ticket
tranportationId
seatId
buyerId
travellerId
cancelled

### Seat
vehicleId
row
column
vipFlag

### Transaction
buyerId
amount?
ticketId
couponId
serialNo


Not Mapped
### Search
### 
### 


## Phases



## Questions
### Authentication and Authorization
### Roles in database, how to handle multiple roles
### DB Management of person sub classes
### Should we complete the whole db 
### How to handle tickets 
