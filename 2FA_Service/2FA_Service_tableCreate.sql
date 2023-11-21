USE [2FA_Service]
GO

/****** Object:  Table [dbo].[PhoneNumberCodes]    Script Date: 11/14/2023 7:06:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PhoneNumberCodes](
	[Id] [int] NOT NULL Primary key identity,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL
) ON [PRIMARY]
GO


