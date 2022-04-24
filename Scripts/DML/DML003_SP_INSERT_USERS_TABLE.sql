/*************************************************************
Object: Stored Procedure
Author: Furkan Coþkun
Script Date: April 17, 2022
Description: insert to db_error table
**************************************************************/
CREATE OR ALTER PROCEDURE sp_insert_users
(
	@name varchar(150),
	@password varchar(150),
	@user_email varchar(150)
)
AS BEGIN
BEGIN TRY
	
	DECLARE @prev_xstate int = XACT_STATE()
	
	IF @prev_xstate = -1
		ROLLBACK TRANSACTION
	ELSE IF @prev_xstate = 1
		SAVE TRANSACTION save_sp_insert_users
	ELSE
		BEGIN TRANSACTION
	
	INSERT INTO users 
	(
		USER_NAME, 
		USER_PASSWORD,
		USER_EMAIL
	)
	VALUES
	(
		@name,
		@password,
		@user_email
	)
	
	DECLARE @curr_xstate int = XACT_STATE()
	
	IF (@curr_xstate) = 1
		COMMIT TRANSACTION

END TRY
BEGIN CATCH
	
	exec dbo.sp_insert_error_log

	IF @curr_xstate = -1
        ROLLBACK TRANSACTION
	ELSE IF @curr_xstate = 1 and @prev_xstate = 0 
		ROLLBACK TRANSACTION
	ELSE IF @curr_xstate = 1 and @prev_xstate = 1 
		ROLLBACK TRANSACTION save_sp_insert_users

END CATCH
END 
