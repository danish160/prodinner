use master
go
--kill all connections to db prodinner
--you might get "Only user processes can be killed." it's ok
DECLARE @dbname sysname
SET @dbname = 'prodinner'
DECLARE @spid int
SELECT @spid = min(spid) from master.dbo.sysprocesses where dbid = db_id(@dbname)
WHILE @spid IS NOT NULL
BEGIN
EXECUTE ('KILL ' + @spid)
SELECT @spid = min(spid) from master.dbo.sysprocesses where dbid = db_id(@dbname) AND spid > @spid
END
go
--recreate the database
drop database prodinner
go
create database prodinner
go
use prodinner
go

create table feedbacks(
id int identity primary key,
comments nvarchar(max))

create table countries(
id int identity primary key,
name nvarchar(20) not null,
isdeleted bit default(0) not null
)

create table meals(
id int identity primary key,
name nvarchar(50) not null,
comments nvarchar(max),
picture nvarchar(40),
isdeleted bit default(0) not null
)

create table chefs(
id int identity primary key,
firstname nvarchar(15) not null,
lastname nvarchar(15) not null,
countryid int references countries(id),
isdeleted bit default(0) not null
)

create table dinners(
id int identity primary key,
name nvarchar(50) not null,
countryid int references countries(id) not null,
chefid int references chefs(id) not null,
"address" nvarchar(50),
start datetime,
"end" datetime,
isdeleted bit default(0) not null
)

create table dinnermeals(
dinnerid int references dinners(id),
mealid int references meals(id),
unique(dinnerid, mealid)
)

create table users(
id int identity primary key,
login nvarchar(15) not null unique,
password nvarchar(40) not null,
isdeleted bit default(0) not null
)

create table roles(
id int identity primary key,
name nvarchar(10)
)

create table userroles(
userid int references users(id) not null,
roleid int references roles(id) not null,
unique(userid, roleid)
)

insert roles values('admin')
insert roles values('role1')
insert roles values('role2')
insert roles values('role3')
insert roles values('role4')

insert users values('admin','rwVPB7HX8AjAoCNVXS+U3WtbY+kuN/cOmAZ042aG',0)
insert users values('super','Ylfui9ZMb18enpkNT3m/LRBJPlT1zePU7BdBlck7',0)
insert users values('pro','yjRolRur0SVrA0iACQaw0pRpfhNeW+qkQp0WhCUE',0)

insert userroles values(1,1)
insert userroles values(1,2)
insert userroles values(1,3)
insert userroles values(2,1)
insert userroles values(3,1)
insert userroles values(3,2)


insert countries(name) values('Moldova')
insert countries(name) values('USA')
insert countries(name) values('United Kingdom')
insert countries(name) values('Belgium')
insert countries(name) values('Germany')
insert countries(name) values('Mexico')
insert countries(name) values('Brazil')
insert countries(name) values('Rohan')
insert countries(name) values('Mordor')
insert countries(name) values('Gondor')
insert countries(name) values('Isengard')
insert countries(name) values('Stormwind')
insert countries(name) values('Redridge')
insert countries(name) values('Goldshire')
insert countries(name) values('Northshire')
insert countries(name) values('Duskwood')
insert countries(name) values('Elwynn Forest')
insert countries(name) values('Westfall')
insert countries(name) values('Northrend')
insert countries(name) values('Kalimdor')
insert countries(name) values('Eastern Kingdoms')
insert countries(name) values('Azeroth')
insert countries(name) values('Outland')
insert countries(name) values('Loch Modan')
insert countries(name) values('Teldrassil')
insert countries(name) values('Felwood')
insert countries(name) values('Durotar')
insert countries(name) values('Feralas')
insert countries(name) values('Tanaris')
insert countries(name) values('Moonglade')


insert meals(name,comments,picture) values('broccoli', 'broccoli as is in its natural form','1.jpg')
insert meals(name,comments,picture) values('broccoli with broccoli', 'broccoli, brocoli leaves, broccoli tree, water, salt and pepper','2.jpg')
insert meals(name,comments,picture) values('broccoli soup', 'pro style broccoli soup','3.jpg')
insert meals(name,comments,picture) values('banana', 'yellow fruit','4.jpg')
insert meals(name,comments,picture) values('pineapple', 'apple with a poohawk','5.jpg')
insert meals(name,comments,picture) values('roast beef', 'roasted beef','6.jpg')
insert meals(name,comments,picture) values('beef steak', 'beef','7.jpg')
insert meals(name,comments,picture) values('chiken soup', 'soup with chicken ','8.jpg')
insert meals(name,comments,picture) values('big broccoli', 'broccoli','9.jpg')
insert meals(name,comments,picture) values('tomatoes', 'red tomatoes','10.jpg')
insert meals(name,comments,picture) values('salad', 'very nice salad','11.jpg')
insert meals(name,comments,picture) values('tree', 'looks like a huge broccoli','12.jpg')
insert meals(name,comments,picture) values('melon', 'yellow melon','13.jpg')
insert meals(name,comments,picture) values('watermelon', 'green, red on the inside','14.jpg')
insert meals(name,comments,picture) values('orange juice', 'juice made from oranges','15.jpg')
insert meals(name,comments,picture) values('strawberries', 'awesome','16.jpg')
insert meals(name,comments,picture) values('coconut water', 'great drink','17.jpg')


insert chefs(firstname,lastname,countryid) values('athene', 'wins', 4)
insert chefs(firstname,lastname,countryid) values('naked', 'chef', 3)
insert chefs(firstname,lastname,countryid) values('chef', 'chef', 2)

set dateformat dmy;

insert dinners(name,countryid,chefid,address, "start","end") values('Food Festival',1,3,'Pro 1337 str.','13/6/2011 10:30', '13/6/2011 11:30')
insert dinners(name,countryid,chefid,address,"start","end") values('Cool gathering',3,2,'doesn''t matter','13/6/2011 10:30', '13/6/2011 11:30')
insert dinners(name,countryid,chefid,address,"start","end") values('Latte Art',5,3,'31337 str.','13/6/2011 10:30', '13/6/2011 11:30')
insert dinners(name,countryid,chefid,address,"start","end") values('Beach Get-away',10,2,'Beach','13/6/2011 10:30', '13/6/2011 11:30')
insert dinners(name,countryid,chefid,address,"start","end") values('Dinner with The Man',13,1,'at home','13/6/2011 10:30', '13/6/2011 11:30')
insert dinners(name,countryid,chefid,address,"start","end") values('Annie''s Spring Fever Lunch',22,3,'picnic','13/6/2011 10:30', '13/6/2011 11:30')
insert dinners(name,countryid,chefid,address,"start","end") values('Blind "start","end"..',27,2,'Location unknown','13/6/2011 10:30', '13/6/2011 11:30')
insert dinners(name,countryid,chefid,address,"start","end") values('Italian Romantic Dinner',4,1,'Antwerpen','13/6/2011 10:30', '13/6/2011 11:30')
insert dinners(name,countryid,chefid,address,"start","end") values('Uber dinner',17,2,'in the Forest','13/6/2011 10:30', '13/6/2011 11:30')
insert dinners(name,countryid,chefid,address,"start","end") values('L337 Dinner',19,1,'internetz','13/6/2011 10:30', '13/6/2011 11:30')

insert dinnermeals(dinnerid,mealid) values(1,1)
insert dinnermeals(dinnerid,mealid) values(1,2)
insert dinnermeals(dinnerid,mealid) values(1,3)
insert dinnermeals(dinnerid,mealid) values(1,4)
insert dinnermeals(dinnerid,mealid) values(1,12)
insert dinnermeals(dinnerid,mealid) values(2,13)
insert dinnermeals(dinnerid,mealid) values(2,11)
insert dinnermeals(dinnerid,mealid) values(2,14)
insert dinnermeals(dinnerid,mealid) values(2,17)
insert dinnermeals(dinnerid,mealid) values(3,4)
insert dinnermeals(dinnerid,mealid) values(3,5)
insert dinnermeals(dinnerid,mealid) values(3,8)
insert dinnermeals(dinnerid,mealid) values(3,16)
insert dinnermeals(dinnerid,mealid) values(4,1)
insert dinnermeals(dinnerid,mealid) values(4,11)
insert dinnermeals(dinnerid,mealid) values(4,13)
insert dinnermeals(dinnerid,mealid) values(4,3)
insert dinnermeals(dinnerid,mealid) values(5,9)
insert dinnermeals(dinnerid,mealid) values(5,10)
insert dinnermeals(dinnerid,mealid) values(5,7)
insert dinnermeals(dinnerid,mealid) values(5,12)
insert dinnermeals(dinnerid,mealid) values(5,6)
insert dinnermeals(dinnerid,mealid) values(6,1)
insert dinnermeals(dinnerid,mealid) values(6,7)
insert dinnermeals(dinnerid,mealid) values(6,3)
insert dinnermeals(dinnerid,mealid) values(6,4)
insert dinnermeals(dinnerid,mealid) values(6,5)
insert dinnermeals(dinnerid,mealid) values(6,8)
insert dinnermeals(dinnerid,mealid) values(7,14)
insert dinnermeals(dinnerid,mealid) values(7,10)
insert dinnermeals(dinnerid,mealid) values(7,2)
insert dinnermeals(dinnerid,mealid) values(7,12)
insert dinnermeals(dinnerid,mealid) values(8,9)
insert dinnermeals(dinnerid,mealid) values(8,2)
insert dinnermeals(dinnerid,mealid) values(8,10)
insert dinnermeals(dinnerid,mealid) values(8,13)
insert dinnermeals(dinnerid,mealid) values(8,3)
insert dinnermeals(dinnerid,mealid) values(9,17)
insert dinnermeals(dinnerid,mealid) values(7,15)
insert dinnermeals(dinnerid,mealid) values(9,12)
insert dinnermeals(dinnerid,mealid) values(9,11)
insert dinnermeals(dinnerid,mealid) values(9,7)
insert dinnermeals(dinnerid,mealid) values(9,1)
insert dinnermeals(dinnerid,mealid) values(9,14)
insert dinnermeals(dinnerid,mealid) values(10,1)
insert dinnermeals(dinnerid,mealid) values(10,2)
insert dinnermeals(dinnerid,mealid) values(10,3)
insert dinnermeals(dinnerid,mealid) values(10,4)
insert dinnermeals(dinnerid,mealid) values(10,5)

select * from chefs
select * from dinners
select * from meals

select * from (dinners
left join dinnermeals on  dinners.id = dinnermeals.dinnerid) 
left join meals on meals.id = dinnermeals.mealid 

select * from dinners, meals, dinnermeals
where meals.id = dinnermeals.mealid and dinners.id = dinnermeals.dinnerid 
or (dinnermeals.dinnerid = null and dinnermeals.mealid = null)


