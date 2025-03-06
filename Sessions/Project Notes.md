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

### Location
id
location
type
cityId

### Vehicle
id
typeId


### LocationType
id
typeId

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

## Tasks

- [x] Review ERD
- [ ] Create a Project 
- [ ] Name + Structure
- [x] Talk about the privacy
- [x] Should we all fork the structure?
- [x] We should keep the site and keep it running
- [x] Picking the react course
- [ ] A quick talk about the plans and timing. 


[https://www.youtube.com/watch?v=SqcY0GlETPk](https://www.youtube.com/watch?v=SqcY0GlETPk)

[https://www.youtube.com/watch?v=g3is3wQK70Q](https://www.youtube.com/watch?v=g3is3wQK70Q)





