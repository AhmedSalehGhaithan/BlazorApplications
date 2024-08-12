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
