CREATE PROCEDURE [dbo].[spPeople_Insert]
	@FirstName NvarChar(50),
	@LastName NvarChar(50)
AS
	begin
	Insert into dbo.people(FirstName,LastName) values(@FirstName,@LastName);
	end
