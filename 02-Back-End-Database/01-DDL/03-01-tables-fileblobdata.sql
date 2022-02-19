USE [InternationalTransaction]
GO

/****** Object:  Table [dbo].[fileblobdata]    Script Date: 2/19/2022 7:22:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[fileblobdata](
	[fileguid] [nvarchar](32) NOT NULL,
	[bloburi] [nvarchar](500) NOT NULL,
	[blobextension] [varchar](5) NULL,
	[goodbadstatus] [int] NOT NULL,
 CONSTRAINT [PK_fileblobdata] PRIMARY KEY CLUSTERED 
(
	[fileguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=bad;1=good' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fileblobdata', @level2type=N'COLUMN',@level2name=N'goodbadstatus'
GO


