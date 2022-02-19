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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_transactionrecord_add_01 
	-- Add the parameters for the stored procedure here
	@fileguid nvarchar(32),
	@linenumber int,
	@bloburi nvarchar(500),
	@transactionid nvarchar(50),
	@amount decimal,
	@currencycode nchar(3),
	@transactiondate datetime2,
	@statuscode nchar(1),
	@istest int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @ret_is_success INT = 0;
	DECLARE @ret_err_msg VARCHAR(50) = '';

	IF NOT EXISTS(
	SELECT 1 FROM [dbo].[transactionrecord] WHERE 
	@fileguid = fileguid AND linenumber = @linenumber
	)
	BEGIN
		INSERT INTO [dbo].[transactionrecord] (
			fileguid		,
			linenumber		,
			bloburi			,
			transactionid	,
			amount			,
			currencycode	,
			transactiondate ,
			statuscode		
		)
		SELECT 
			@fileguid		 fileguid		  ,
			@linenumber		 linenumber		  ,
			@bloburi		 bloburi		  ,
			@transactionid	 transactionid	  ,
			@amount			 amount			  ,
			@currencycode	 currencycode	  ,
			@transactiondate transactiondate  ,
			@statuscode		 statuscode		
	END
	ELSE BEGIN

		SET @ret_is_success = 1;
		IF @istest = 1 
		BEGIN
			SET @ret_err_msg = 'Skipped Insert due to test';
		END
		ELSE BEGIN
			SET @ret_err_msg = 'Skipped Insert due to duplicate';
		END

	END 

    -- Insert statements for procedure here
	SELECT 1 [is_success]
END
GO
