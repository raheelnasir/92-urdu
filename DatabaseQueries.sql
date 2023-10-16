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


	
	

