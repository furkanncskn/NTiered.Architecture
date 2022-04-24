/*************************************************************
Object: Stored Procedure
Author: Furkan Coþkun
Script Date: April 25, 2022
Description: Get Active Users
**************************************************************/
CREATE OR ALTER PROCEDURE sp_get_users
AS BEGIN

	SELECT 
		USER_ID,
		USER_NAME,
		dbo.[hide_text](USER_PASSWORD, '*', 2,0) AS 'USER_PASSWORD',
		USER_EMAIL,
		USER_REGISTER_DATE,
		USER_IS_ACTIVE
	FROM Users 
	WHERE USER_IS_ACTIVE = 1

END