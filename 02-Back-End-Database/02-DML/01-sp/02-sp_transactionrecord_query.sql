-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================

DROP PROCEDURE IF EXISTS sp_transactionrecord_query 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE sp_transactionrecord_query 
	-- Add the parameters for the stored procedure here
	@querymode varchar(25),
	@value01 nvarchar(100),
	@value02 nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @date01 DATETIME2 = NULL;
	DECLARE @date02 DATETIME2 = NULL;

	IF @querymode = 'TransactionsByCurrency' 
	AND @value01 IS NOT NULL
	BEGIN
		SELECT 
								1					[isquerysuccess],	
								NULL				[errormessage], 
		[transactionid]								[id], 
		CONCAT_WS(
			' ', 
			CONVERT(DECIMAL(10,2), [amount]), 
			[currencycode]
		)											[payment], 
		[statuscode]								[status] 
		FROM [dbo].[transactionrecord] where 
		[currencycode] = @value01
	END
	ELSE IF @querymode = 'TransactionsByDateRange'
	AND @value01 IS NOT NULL
	AND @value02 IS NOT NULL
	BEGIN
		SET @date01 = CONVERT(DATETIME2, @value01, 126) 
		SET @date02 = CONVERT(DATETIME2, @value02, 126)
		SELECT 
								1					[isquerysuccess],	
								NULL				[errormessage], 
		[transactionid]								[id], 
		CONCAT_WS(
			' ', 
			CONVERT(DECIMAL(10,2), [amount]), 
			[currencycode]
		)											[payment], 
		[statuscode]								[status] 
		FROM [dbo].[transactionrecord] where 
		[transactiondate] >= @date01		
		AND [transactiondate] <= @date02
	END
	ELSE IF @querymode = 'TransactionsByStatus'
	AND @value01 IS NOT NULL
	BEGIN
		SELECT 
								1					[isquerysuccess],	
								NULL				[errormessage], 
		[transactionid]								[id], 
		CONCAT_WS(
			' ', 
			CONVERT(DECIMAL(10,2), [amount]), 
			[currencycode]
		)											[payment], 
		[statuscode]								[status] 
		FROM [dbo].[transactionrecord] where 
		[statuscode] = @value01		
	END
	ELSE BEGIN
	SELECT 0 isquerysuccess, 'Query mode is invalid. ' errormessage
	END
END
GO
