/*************************************************************
Object: Function
Author: anonymous 
Script Date: April 17, 2022
Description: base64 encode
**************************************************************/
CREATE OR ALTER FUNCTION base64_encode(@str varchar(4000))
RETURNS varchar(4000) 
AS BEGIN
DECLARE @decode_str varchar(4000)

	SET @decode_str = (
				SELECT
					CAST(N'' AS XML).value 
					( 
						'xs:base64Binary(xs:hexBinary(sql:column("bin")))' , 
						'VARCHAR(MAX)'
					) Base64Encoding
				FROM 
					(
						SELECT CAST(@str AS VARBINARY(MAX)) AS bin
					) AS bin_sql_server_temp 
				)

	RETURN @decode_str
END