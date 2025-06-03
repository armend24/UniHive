CREATE PROCEDURE sp_LoginUser
	@Username NVARCHAR(50),
	@Password NVARCHAR (50)
AS
BEGIN
	SELECT UserID, FullName, Role 
	FROM Users
	WHERE Username = @Username AND Password = @Password AND IsActive = True
END


--Procedure test:
--exec sp_LoginUser '*****', '***'


USE UniversityICT
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_LoginUser]
	@Username NVARCHAR(50),
	@Password NVARCHAR(50)
AS
BEGIN
	SELECT UserID, FullName, Role 
	FROM Users
	WHERE Username = @Username AND Password = @Password AND IsActive = 1
END
