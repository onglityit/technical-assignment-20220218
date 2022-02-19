USE [InternationalTransaction]
GO

/****** Object:  Table [dbo].[transactionrecord]    Script Date: 2/19/2022 5:57:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[transactionrecord](
	[fileguid] [nvarchar](32) NOT NULL,
	[linenumber] [int] NOT NULL,
	[bloburi] [nvarchar](500) NOT NULL,
	[transactionid] [nvarchar](50) NOT NULL,
	[amount] [decimal](18, 2) NOT NULL,
	[currencycode] [nchar](3) NOT NULL,
	[transactiondate] [datetime2](7) NOT NULL,
	[statuscode] [nchar](1) NOT NULL,
 CONSTRAINT [PK_transactionrecord] PRIMARY KEY CLUSTERED 
(
	[fileguid] ASC,
	[linenumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO