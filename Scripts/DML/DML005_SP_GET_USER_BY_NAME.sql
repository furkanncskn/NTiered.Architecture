/*************************************************************
Object: Stored Procedure
Author: Furkan Coþkun
Script Date: April 25, 2022
Description: Get user by name 
**************************************************************/
CREATE OR ALTER PROCEDURE sp_get_user_by_name
(
	@USER_NAME varchar(150)
)
AS BEGIN

	SELECT 
		USER_ID,
		USER_NAME,
		dbo.[hide_text](USER_PASSWORD, '*', 2,0) AS 'USER_PASSWORD',
		USER_EMAIL,
		USER_REGISTER_DATE,
		USER_IS_ACTIVE
	FROM Users 
	WHERE 
		USER_NAME = @USER_NAME and 
		USER_IS_ACTIVE = 1

END
