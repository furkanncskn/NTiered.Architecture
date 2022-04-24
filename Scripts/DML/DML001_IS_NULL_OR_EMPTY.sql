/*************************************************************
Object: Function
Author: Furkan Coþkun
Script Date: April 17, 2022
Description: if null or empty then 0, otherwise 1
**************************************************************/
CREATE OR ALTER FUNCTION IS_NULL_OR_EMPTY(@str varchar(max))
RETURNS BIT
AS BEGIN
	
	IF LEN(@str) > 0
		RETURN 1

	RETURN 0

END
