if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cm]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[cm]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cu]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[cu]
GO

CREATE TABLE [dbo].[cm] (
	[CalledNumber] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[CallingNumber] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[CallTime] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO
Insert into cm values ('13912345678','13987654321','     120')
Insert into cm values ('13912345688','13987654322','     160')
Insert into cm values ('13912345698','13987654322','      20')
go

CREATE TABLE [dbo].[cu] (
	[CallingNumber] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[CalledNumber] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[CallTime] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO
Insert into cu values (' 13912345678',' 13987654321','     120')
Insert into cu values (' 13912345688',' 13987654322','     160')
Insert into cu values (' 13912345698',' 13987654322','      20')
go

