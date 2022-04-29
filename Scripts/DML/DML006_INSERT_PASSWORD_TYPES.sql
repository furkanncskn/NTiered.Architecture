/*************************************************************
Object: Insert Into Table
Author: Furkan Coþkun
Script Date: April 17, 2022
Description: 

	0 : 'Herhangi seçim kriteri yok'
	1 : 'Ayda bir şifre yenilenmeli'
	2 : '3 Ayda bir şifre yenilenmeli'	
	3 : 'Yılda bir şifre yenilenmeli'

**************************************************************/
INSERT INTO password_types(PASSWORD_TYPE_ID, PASSWORD_DESCRIPTION)
VALUES
(0, '"Herhangi seçim kriteri yok"'),
(1, '"Bir ayda bir şifre yenilenmeli"'),
(2, '"Üç ayda bir şifre yenilenmeli"'),
(3, '"Bir yılda bir şifre yenilenmeli"')