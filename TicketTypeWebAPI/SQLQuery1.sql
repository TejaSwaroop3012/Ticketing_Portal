CREATE DATABASE TicketPriorityDB
USE TicketPriorityDB
CREATE TABLE TicketPriority (
PriorityId INT PRIMARY KEY,
PriorityName VARCHAR(20)  NOT NULL,
RespondWithin INT  NOT NULL,
ResolveWithin INT  NOT NULL
);