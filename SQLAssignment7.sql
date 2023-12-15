create database LibraryDB
use LibraryDB
create table Books
(BookId int primary key,
Title nvarchar(50),
Author nvarchar(50),
Genre nvarchar(50),
Quantity int)

insert into Books values(1,'Wings Of Fire','APJ','LifeStory',3)
select * from Books