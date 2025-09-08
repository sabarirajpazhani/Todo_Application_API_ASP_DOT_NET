create database todoApplicationDB;

use todoApplicationDB;

create table todos (
	id int identity(1,1) primary key,
	Title varchar(90) not null,
	Description varchar(100) not null,
	isComplete varchar(20) default 'false'
);

insert into todos values 
('Coding', 'Practicing Leetcode Problems', 'flase'),
('Developing', 'Develop the SMS system', 'true');


select * from todos;