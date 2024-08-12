CREATE PROCEDURE [dbo].[spPeople_Delete]
	@Id int
AS
	begin
		delete from dbo.people where Id = @Id
	end
