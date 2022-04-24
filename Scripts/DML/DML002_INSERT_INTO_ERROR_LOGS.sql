/*************************************************************
Object: Stored Procedure
Author: Furkan Coþkun
Script Date: April 17, 2022
Description: insert to db_error table
**************************************************************/
CREATE OR ALTER PROCEDURE sp_insert_error_log
AS BEGIN
BEGIN TRY

	INSERT INTO dbo.error_logs
    VALUES
	(
		SUSER_SNAME(),
		ERROR_NUMBER(),
		ERROR_STATE(),
		ERROR_SEVERITY(),
		ERROR_LINE(),
		ERROR_PROCEDURE(),
		ERROR_MESSAGE(),
		GETDATE()
	)

END TRY
BEGIN CATCH

	DECLARE @error_sp varchar(500) = ERROR_PROCEDURE()

	RAISERROR('Unexpected Error: %s',16,1, @error_sp)

END CATCH
END