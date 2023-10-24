-- Tables
EXEC sp_rename 'UserProfile', 'UserAuth';
drop table UserAuth
create table UserAuth(
    UId INT  PRIMARY KEY,
	UserName varchar(50),
	Password varchar(50),
	Role varchar(20)
)
select * from UserAuth
select * from UserProfile;

CREATE TABLE UserProfile (
    UId INT,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    EmailAddress VARCHAR(100),
    PhoneNumber VARCHAR(15),
    DateOfBirth VARCHAR(25),
    City VARCHAR(25),
    Area VARCHAR(25),
    Location VARCHAR(25),
    IsActive BIT,
    FOREIGN KEY (UId) REFERENCES UserAuth(UId)
);


-- Commands
	use Project
	select * from UserProfile up join UserAuth ua 
	on up.UId = ua.UId


-- Stored Procedures
-- Create a stored procedure to delete a user by UId

alter PROCEDURE sp_DeleteUsersProfile 
    @uid INT,
	@role NVARCHAR(20),
	@outputMessage NVARCHAR(100) = NULL OUTPUT
AS
BEGIN
    -- Begin a transaction to ensure data consistency
    BEGIN TRANSACTION;

    -- Delete from the UserProfile table
	IF @role = 'Administrator' or @role = 'Editor' or @role = 'Cheif Editor'
	BEGIN
    DELETE FROM UserProfile
    WHERE UId = @uid;

    -- Delete from the UserAuth table
    DELETE FROM UserAuth
    WHERE UId = @uid;
	END

    -- Commit the transaction
    COMMIT;

    -- If any error occurs, rollback the transaction
    IF @@ERROR <> 0
    BEGIN
        ROLLBACK;
    END;
END;

	create Procedure sp_GetUsersData
	@role NVARCHAR(20)
	AS
	Begin
	IF @role = 'Editor' or @role='Administrator' or @role='Cheif Editor'
	BEGIN
	select * from UserProfile up join UserAuth ua 
	on up.UId = ua.UId
	RETURN
	END
	END
	sp_GetUsersData 'Editor'
	
	alter PROCEDURE sp_UpdateUsersProfileData
    @uid INT,
    @username NVARCHAR(255),
    @role NVARCHAR(50) = NULL,
    @isactive BIT = NULL,
		@outputMessage NVARCHAR(100) = NULL OUTPUT


AS
BEGIN
    -- Update Role in UserAuth table if @role is not NULL
    IF @role IS NOT NULL
    BEGIN
        UPDATE UserAuth
        SET Role = @role
        WHERE UserName = @username AND UId = @uid;
    END

    -- Update IsActive in UserProfile table if @isactive is not NULL
    IF @isactive IS NOT NULL
    BEGIN
        UPDATE UserProfile
        SET IsActive = @isactive
        WHERE  UId = @uid;
    END
END





	CREATE PROCEDURE sp_LoginUserAuth
		@username NVARCHAR(50),

		@password NVARCHAR(50)
	AS
	BEGIN
		-- Check if the provided username and password match a user in the UserProfile table.
		SELECT UserName,Role, UId  
		FROM UserAuth where  UserName = @username and Password = @password 
	END
	EXECUTE sp_LoginUserAuth @username = 'admin', @password = 'admin';


create
ALTER PROCEDURE sp_SignupUser
	@uid NVARCHAR(50),
	@username NVARCHAR(50),
	@password NVARCHAR(50),
	@role NVARCHAR(20),
	@firstname NVARCHAR(50),
	@lastname NVARCHAR(50),
	@emailaddress NVARCHAR(50),
	@phonenumber NVARCHAR(50),
	@dateofbirth NVARCHAR(50),
	@city NVARCHAR(50),
	@area NVARCHAR(50),
	@location NVARCHAR(50),
	@isactive BIT,
	@outputMessage NVARCHAR(100) = NULL OUTPUT
AS
BEGIN
	DECLARE @usernameExists BIT;
	DECLARE @emailExists BIT;

	-- Check if the username already exists
	SELECT @usernameExists = CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END
	FROM UserAuth
	WHERE UserName = @username;

	-- Check if the email address already exists
	SELECT @emailExists = CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END
	FROM UserProfile
	WHERE EmailAddress = @emailaddress;

	-- If either username or email already exists, set the output message and do not proceed with insertion
	IF @usernameExists = 1 
	BEGIN
		SET @outputMessage = 'Username already exists';
		RETURN;
	END

	IF  @emailExists = 1
	BEGIN
		SET @outputMessage = 'Email already exists';
		RETURN;
	END

	-- If neither username nor email exists, proceed with insertion
	INSERT INTO UserAuth
	VALUES (@uid, @username, @password, @role);

	INSERT INTO UserProfile
	VALUES (@uid, @firstname, @lastname, @emailaddress, @phonenumber, @dateofbirth, @city, @area, @location, @isactive);

	SET @outputMessage = 'User registered successfully';
END

create
ALTER PROCEDURE sp_CreateUser
	@uid NVARCHAR(50),
	@username NVARCHAR(50),
	@password NVARCHAR(50),
	@role NVARCHAR(20)= NULL,
	@firstname NVARCHAR(50),
	@lastname NVARCHAR(50),
	@emailaddress NVARCHAR(50),
	@phonenumber NVARCHAR(50)= NULL,
	@dateofbirth NVARCHAR(50)= NULL,
	@city NVARCHAR(50)= NULL,
	@area NVARCHAR(50)= NULL,
	@location NVARCHAR(50)= NULL,
	@isactive BIT= NULL,
	@outputMessage NVARCHAR(100) = NULL OUTPUT
AS
BEGIN
    DECLARE @usernameExists BIT;
    DECLARE @emailExists BIT;

    -- Check if the username already exists
    SELECT @usernameExists = CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END
    FROM UserAuth
    WHERE UserName = @username;

    -- Check if the email address already exists
    SELECT @emailExists = CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END
    FROM UserProfile
    WHERE EmailAddress = @emailaddress;

    -- If either username or email already exists, set the output message and do not proceed with insertion
    IF @usernameExists = 1 
    BEGIN
        SET @outputMessage = 'Username already exists';
        RETURN;
    END

    IF @emailExists = 1
    BEGIN
        SET @outputMessage = 'Email already exists';
        RETURN;
    END

    -- If neither username nor email exists, proceed with insertion
    INSERT INTO UserAuth
    VALUES (@uid, @username, @password, @role);

    -- Insert only the specified columns
   	INSERT INTO UserProfile
	VALUES (@uid, @firstname, @lastname, @emailaddress, @phonenumber, @dateofbirth, @city, @area, @location, @isactive);

    SET @outputMessage = 'User registered successfully';
END


-- Execute the stored procedure with random data


	
	

	alter Procedure sp_GetPoetData 197485, 'Poet'
ALTER PROCEDURE sp_GetPoetData
    @uid int,
    @role NVARCHAR(20)
AS
BEGIN
    IF @role = 'Poet'
    BEGIN
        -- Return all data for the user with the specified UID
        SELECT  ua.UserName,up.FirstName, up.LastName, up.EmailAddress, up.PhoneNumber, up.DateOfBirth, up.City, up.Area, up.Location
        FROM UserProfile up
        JOIN UserAuth ua ON up.UId = ua.UId
        WHERE ua.UId = @uid
    END
END
create table WordDictionary(
DId int  ,
 DWord nvarchar(20) ,
 DMeaning nvarchar(50),
)
insert into WordDictionary
Values(1,'????','???? ?? ??? ?????? ???? ????'),(2,'????','????? ?? ???? ????'),(3,'??','???? ?? ?? ????? ??');
select * from WordDictionary

use Project
select distinct(pid), sher from SheroShairi
SELECT pid, STRING_AGG(sher, ' ') AS ConcatenatedSher
FROM SheroShairi
GROUP BY pid;

create procedure sp_GetWordDictionary
AS
BEGIN
select * from WordDictionary
END










create table ContentDetails (
UId int,
ContentName varchar(50),
ContentArrangement varchar(15),
ContentId int  primary key,
ContentType varchar(1),
foreign key (UId) references UserAuth(UId)
)

create table Verses (
ContentId int,
VerseId int identity(1,1) primary key,
Verse varchar(50),
foreign key (ContentId) references ContentDetails(ContentId)
)
select * from ContentDetails

alter procedure sp_SetContentDetails
@uid int,
@contentname nvarchar(50),
@contentarrangement nvarchar(15),
@contentid int,
@contenttype nvarchar(1),
@outputMessage nvarchar(100) = NULL OUTPUT
AS
BEGIN
insert into ContentDetails(UId,ContentName,ContentArrangement,ContentId,ContentType)
Values(@uid,@contentname,@contentarrangement,@contentid,@contenttype)
END

alter procedure sp_PostVerse 
@contentid int,
@verse varchar(50),
@outputMessage nvarchar(100) = NULL OUTPUT
AS
BEGIN
insert into Verses(ContentId, Verse)
Values (@contentid,@verse)
END
select * from Verses

-- Couplets for Content 1 (Poem 1)
INSERT INTO Verses(ContentId, Verse) VALUES
(1, 'Verse 1 of Poem 1'),
(1, 'Verse 2 of Poem 1'),
(1, 'Verse 3 of Poem 1');

-- Couplets for Content 2 (Song 1)
INSERT INTO Couplet (ContentId, Verse) VALUES
(2, 'Verse 1 of Song 1'),
(2, 'Verse 2 of Song 1');

-- Couplets for Content 3 (Quote 1)
INSERT INTO Couplet (ContentId, Verse) VALUES
(3, 'Quote 1');

SELECT 
    U.UserId AS UserID, 
    CD.ContentId AS ContentID, 
    CD.ContentName, 
    CD.Type, 
    CO.VerseId AS VerseID, 
    CO.Verse
FROM UserProfile U
INNER JOIN ContentDetails CD ON U.UserId = CD.UserId
LEFT JOIN Couplet CO ON CD.ContentId = CO.ContentId
ORDER BY U.UserId, CD.ContentId, CO.VerseId;



SELECT
    U.UserId AS UserID,
    CD.ContentId AS ContentID,
    CD.ContentName,
    CD.Type,
    STRING_AGG(CO.Verse, ', ') AS Verses
FROM UserProfile U
INNER JOIN ContentDetails CD ON U.UserId = CD.UserId
LEFT JOIN   CO ON CD.ContentId = CO.ContentId
GROUP BY U.UserId, CD.ContentId, CD.ContentName, CD.Type
ORDER BY U.UserId, CD.ContentId;


SELECT
    U.UserId AS UserID,
    CD.ContentId AS ContentID,
    CD.ContentName,
    CD.Type,
    STRING_AGG(CO.Verse, ', ') WITHIN GROUP (ORDER BY CO.VerseId) AS Verses
FROM UserProfile U
INNER JOIN ContentDetails CD ON U.UserId = CD.UserId
LEFT JOIN Couplet CO ON CD.ContentId = CO.ContentId
GROUP BY U.UserId, CD.ContentId, CD.ContentName, CD.Type
ORDER BY U.UserId, CD.ContentId;


-- Insert data into ContentDetails
INSERT INTO ContentDetails (UId, ContentName, ContentArrangement, ContentId, ContentType)
VALUES (197485, 'Sample Content 1', '4-4', 1, 'P');

-- Insert data into Verses for ContentId 1
INSERT INTO Verses (ContentId, Verse)
VALUES (1, 'Verse 1');
INSERT INTO Verses (ContentId, Verse)
VALUES (1, 'Verse 2');
INSERT INTO Verses (ContentId, Verse)
VALUES (1, 'Verse 3');
INSERT INTO Verses (ContentId, Verse)
VALUES (1, 'Verse 4');

-- Insert data into ContentDetails for another content
INSERT INTO ContentDetails (UId, ContentName, ContentArrangement, ContentId, ContentType)
VALUES (197485, 'Sample Content 2', 'Random', 2, 'T');

-- Insert data into Verses for ContentId 2
INSERT INTO Verses (ContentId, Verse)
VALUES (2, 'Verse A');
INSERT INTO Verses (ContentId, Verse)
VALUES (2, 'Verse B');
INSERT INTO Verses (ContentId, Verse)
VALUES (2, 'Verse C');
INSERT INTO Verses (ContentId, Verse)
VALUES (2, 'Verse D');

SELECT CD.UId, CD.ContentType, CD.ContentArrangement, CD.ContentName, STRING_AGG(V.Verse, ', ') as Verses
FROM ContentDetails CD
JOIN Verses V ON CD.ContentId = V.ContentId
GROUP BY CD.UId, CD.ContentType, CD.ContentArrangement, CD.ContentName;


alter procedure sp_GetPotry

AS
BEGIN

SELECT CD.UId, CD.ContentType, CD.ContentArrangement, CD.ContentName, STRING_AGG(V.Verse, ' , ') as Verses,
       CONCAT(UP.FirstName, ' ', UP.LastName) AS FullName, UA.UserName
FROM ContentDetails CD
JOIN Verses V ON CD.ContentId = V.ContentId
JOIN UserProfile UP ON CD.UId = UP.UId
JOIN UserAuth UA ON CD.UId = UA.UId
GROUP BY CD.UId, CD.ContentType, CD.ContentArrangement, CD.ContentName, UP.FirstName, UP.LastName, UA.UserName;
END