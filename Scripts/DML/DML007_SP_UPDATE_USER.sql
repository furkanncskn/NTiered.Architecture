/*************************************************************
Object: Stored Procedure
Author: Furkan Coþkun
Script Date: April 25, 2022
Description: Update user informations
**************************************************************/
CREATE OR ALTER PROCEDURE sp_update_user
(
	@USER_ID int,
	@USER_NAME varchar(150),
	@USER_PASSWORD varchar(150),
	@USER_EMAIL varchar(150)
)
AS BEGIN
BEGIN TRY
	
	DECLARE @prev_xstate int = XACT_STATE()
	
	IF @prev_xstate = -1 
		ROLLBACK TRANSACTION
	ELSE IF @prev_xstate = 0
		BEGIN TRANSACTION
	ELSE 
		SAVE TRANSACTION sp_update_user
	
	UPDATE Users 
	SET 
		USER_NAME = @USER_NAME,
		USER_PASSWORD = @USER_PASSWORD,
		USER_EMAIL = @USER_EMAIL
	WHERE 
		USER_ID = @USER_ID
	
	DECLARE @curr_xstate int = XACT_STATE()
	
	IF @curr_xstate = 1
		COMMIT TRANSACTION
	
	RETURN 1

END TRY
BEGIN CATCH
	
	EXEC dbo.sp_insert_error_log

	IF @curr_xstate = -1 
		ROLLBACK TRANSACTION
	ELSE IF @curr_xstate = 1 AND @prev_xstate = 0
		ROLLBACK TRANSACTION
	ELSE IF @curr_xstate = 1 AND @prev_xstate = 1
		ROLLBACK TRANSACTION sp_update_user

	RETURN 0

END CATCH
END