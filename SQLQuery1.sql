USE TicketDB
drop table Ticket

CREATE TABLE Ticket (
TicketId INT IDENTITY(1,1) PRIMARY KEY,
EmpId INT,
TicketTypeId INT,
Subject VARCHAR(50),
Description VARCHAR(100),
CreatedDate DATE DEFAULT GETDATE(),
FOREIGN KEY (EmpId) REFERENCES Employee(EmpId),
FOREIGN KEY (TicketTypeId) REFERENCES TicketType(TicketTypeId)
);
use TicketFollowupDB
DROP TABLE TicketFollowup
DROP TABLE Ticket
CREATE TABLE Ticket (
TicketId INT PRIMARY KEY
)
CREATE TABLE TicketFollowup (
TicketId INT,
SrNo INT,
Status VARCHAR(50),
UpdatedDate DATE DEFAULT GETDATE(),
Remarks varchar(100),
PRIMARY KEY (TicketId, SrNo),
FOREIGN KEY (TicketId) REFERENCES Ticket(TicketId)
)