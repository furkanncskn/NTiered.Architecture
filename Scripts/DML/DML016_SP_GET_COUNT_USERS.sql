CREATE OR ALTER PROCEDURE sp_get_count_users
AS BEGIN
 SET NOCOUNT ON
	RETURN (SELECT COUNT(*) FROM Users)
 SET NOCOUNT OFF
END