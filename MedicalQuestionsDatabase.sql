create database MedicalQuestionsDatabase
go

use MedicalQuestionsDatabase
go

create table Categories(
CategoryID int primary key identity(1,1),
CategoryName nvarchar(max))
go

create table Users(
UserID int primary key identity(1,1),
Email nvarchar(max),
PasswordHash nvarchar(max),
Name nvarchar(max),
Mobile nvarchar(max),
IsAdmin bit default(0))
go

create table Questions(
QuestionID int primary key identity(1,1),
QuestionName nvarchar(max),
QuestionDateAndTime datetime,
UserID int references Users(UserID) on delete cascade,
CategoryID int references Categories(CategoryID) on delete cascade,
VotesCount int,
AnswersCount int,
ViewsCount int)
go

create table Answers(
AnswerID int primary key identity(1,1),
AnswerText nvarchar(max),
AnswerDateAndTime datetime,
UserID int references Users(UserID),
QuestionID int references Questions(QuestionID) on delete cascade,
VotesCount int)
go

create table Votes(
VoteID int primary key identity(1,1),
UserID int references Users(UserID),
AnswerID int references Answers(AnswerID) on delete cascade,
VoteValue int)
go

use MedicalQuestionsDatabase
go

insert into Users values('admin@gmail.com','240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9','Admin','0000000000', 1)
go
insert into Users values('staff@gmail.com','10176E7B7B24D317ACFCF8D2064CFD2F24E154F7B5A96603077D5EF813D6A6B6','Staff','1111111111', 0)
go

insert into Categories values('Skin Fungus')
go
insert into Categories values('Measles')
go
insert into Categories values('Backache')
go
insert into Categories values('Headache')
go
insert into Categories values('Stomachache')
go
insert into Categories values('Sore Throat')
go
insert into Categories values('Cough')
go
insert into Categories values('Cold')
go
insert into Categories values('Runny Nose')
go
insert into Categories values('Flu')
go
insert into Categories values('Fever')
go

insert into Questions values('How to treat skin fungus?','2022-4-25 11:10pm', 2, 1, 0, 0, 0)
go
insert into Questions values('How to treat measles?','2022-4-26 10:10am', 2, 2, 0, 0, 0)
go
