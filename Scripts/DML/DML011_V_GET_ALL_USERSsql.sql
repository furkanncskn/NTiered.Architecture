CREATE OR ALTER VIEW v_get_all_users
AS 
	SELECT 
		USER_ID,
		USER_NAME,
		dbo.[hide_text](USER_PASSWORD, '*', 2,0) AS 'USER_PASSWORD',
		USER_EMAIL,
		USER_REGISTER_DATE,
		USER_IS_ACTIVE
	FROM Users 
	WHERE USER_IS_ACTIVE = 1
	WITH CHECK OPTION