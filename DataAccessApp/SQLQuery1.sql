create database DataAccessDB
use DataAccessDB
Go

create Table people(
Id int primary key identity,
FirstName nvarchar(50) not null,
LastName nvarchar(50) not null
)
GO


CREATE PROCEDURE [dbo].[spPeople_GetAll]
	
AS
Begin
	select Id,FirstName,LastName from dbo.people
End
GO

CREATE PROCEDURE [dbo].[spPeople_Insert]
	@FirstName NvarChar(50),
	@LastName NvarChar(50)
AS
	begin
	Insert into dbo.people(FirstName,LastName) values(@FirstName,@LastName)
	end
GO


CREATE PROCEDURE [dbo].[spPeople_Update]
	@Id int ,
	@FirstName NvarChar(50),
	@LastName NvarChar(50)
AS
	begin
	Update dbo.people
		set FirstName = @FirstName,LastName = @LastName
	where Id = @Id
	end
GO


CREATE PROCEDURE [dbo].[spPeople_Delete]
	@Id int
AS
	begin
		delete from dbo.people where Id = @Id
	end
GO


Select * from people