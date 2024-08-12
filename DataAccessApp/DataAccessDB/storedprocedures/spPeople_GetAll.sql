CREATE PROCEDURE [dbo].[spPeople_GetAll]
	
AS
Begin
	select Id,FirstName,LastName from dbo.people
End
