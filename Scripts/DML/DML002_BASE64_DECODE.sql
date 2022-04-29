/*************************************************************
Object: Function
Author: anonymous 
Script Date: April 17, 2022
Description: base64 decode
**************************************************************/
CREATE OR ALTER FUNCTION base64_decode(@str varchar(4000))
RETURNS varchar(4000) 
AS BEGIN

	DECLARE @encode_str varchar(4000)

	SET @encode_str = 
				(
					SELECT 
						CAST
						(
							CAST(N'' AS XML).value
							(
								'xs:base64Binary(sql:column("bin"))',
								'VARBINARY(MAX)'
							) AS VARCHAR(MAX)
						) ASCIIEncoding
					FROM 
					(
						SELECT CAST(@str AS VARCHAR(MAX)) AS bin
					) AS bin_sql_server_temp
				)

	RETURN @encode_str

END