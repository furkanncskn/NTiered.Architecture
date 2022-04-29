/*************************************************************
Object: Stored Procedure
Author: Furkan Coþkun
Script Date: April 25, 2022
Description: Hide Text, Left and Right
**************************************************************/
CREATE OR ALTER FUNCTION hide_text
(
	@str varchar(4000), 
	@ch char, 
	@left_cnt int, 
	@right_cnt int
)
RETURNS varchar(max)
AS BEGIN

	RETURN LEFT(@str, @left_cnt) + REPLICATE('*', LEN(@str) - @left_cnt - @right_cnt) + RIGHT(@str, @right_cnt)

END