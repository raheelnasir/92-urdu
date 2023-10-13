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
drop table UserProfile
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



-- Stored Procedures

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


create Procedure sp_SignupUser
	@uid  NVARCHAR(50),
    @username NVARCHAR(50),
	@password NVARCHAR(50),
	@role		NVARCHAR(20),
    @firstname NVARCHAR(50),
    @lastname NVARCHAR(50),
    @emailaddress NVARCHAR(50),
    @phonenumber NVARCHAR(50),
	@dateofbirth NVARCHAR(50),
    @city NVARCHAR(50),
    @area NVARCHAR(50),
    @location NVARCHAR(50),
	@isactive bit
	As
	BEGIN

	INSERT INTO UserAuth
	Values(@uid, @username,@password,@role)
	Insert Into UserProfile
	Values(@uid,@firstname,@lastname,@emailaddress,@phonenumber,@dateofbirth,@city,@area,@location,@isactive);
	END
-- Execute the stored procedure with random data




EXEC sp_SignupUser 
    @uid = '123456', -- Replace with a unique identifier for the user
    @username = 'johndoe', -- Replace with the desired username
    @password = 'password123', -- Replace with the desired password
    @role = 'User', -- Replace with the desired role
    @firstname = 'John', -- Replace with the first name
    @lastname = 'Doe', -- Replace with the last name
    @emailaddress = 'johndoe@example.com', -- Replace with the email address
    @phonenumber = '123-456-7890', -- Replace with the phone number
    @dateofbirth = '1990-01-15', -- Replace with the date of birth
    @city = 'New York', -- Replace with the city
    @area = 'Downtown', -- Replace with the area
    @location = '123 Main St', -- Replace with the location
    @isactive = 1; -- 1 for active, 0 for inactive

	use Project
select * from UserProfile up join UserAuth ua 
	on up.UId = ua.UId
	