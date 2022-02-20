USE [InternationalTransaction]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [IDX_transactionrecord_currencycode]    Script Date: 2/20/2022 10:16:14 AM ******/
CREATE NONCLUSTERED INDEX [IDX_transactionrecord_currencycode] ON [dbo].[transactionrecord]
(
	[currencycode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


/****** Object:  Index [IDX_transactionrecord_transactiondate]    Script Date: 2/20/2022 10:17:49 AM ******/
CREATE NONCLUSTERED INDEX [IDX_transactionrecord_transactiondate] ON [dbo].[transactionrecord]
(
	[transactiondate] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


/****** Object:  Index [IDX_transactionrecord_statuscode]    Script Date: 2/20/2022 10:18:30 AM ******/
CREATE NONCLUSTERED INDEX [IDX_transactionrecord_statuscode] ON [dbo].[transactionrecord]
(
	[statuscode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

