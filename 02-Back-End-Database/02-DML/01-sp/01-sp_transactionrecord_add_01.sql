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
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
DROP PROCEDURE IF EXISTS sp_transactionrecord_add_01 ;
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE sp_transactionrecord_add_01 
	-- Add the parameters for the stored procedure here
	@fileguid nvarchar(32),
	@linenumber int,
	@bloburi nvarchar(500),
	@blobextension varchar(5),
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
	SELECT 1 FROM [dbo].[fileblobdata] WHERE 
	@fileguid = fileguid
	)
	BEGIN
		INSERT INTO [dbo].[fileblobdata] (
			fileguid		,
			bloburi			,
			blobextension	,
			goodbadstatus
		)
		SELECT 
			@fileguid		 fileguid		  ,
			@bloburi		 bloburi		  ,
			@blobextension	 blobextension	  ,
			1				 goodbadstatus
	END
	ELSE BEGIN
		SET @ret_is_success = 1;
		IF @istest = 1 
		BEGIN
			SET @ret_err_msg = @ret_err_msg + 'Skipped Insert fileblobdata due to test. ';
		END
	END 

	IF NOT EXISTS(
	SELECT 1 FROM [dbo].[transactionrecord] WHERE 
	@fileguid = fileguid AND linenumber = @linenumber
	)
	BEGIN
		INSERT INTO [dbo].[transactionrecord] (
			fileguid		,
			linenumber		,
			transactionid	,
			amount			,
			currencycode	,
			transactiondate ,
			statuscode		
		)
		SELECT 
			@fileguid		 fileguid		  ,
			@linenumber		 linenumber		  ,
			@transactionid	 transactionid	  ,
			@amount			 amount			  ,
			@currencycode	 currencycode	  ,
			@transactiondate transactiondate  ,
			@statuscode		 statuscode		
		SET @ret_is_success = 1;
	END
	ELSE BEGIN

		SET @ret_is_success = 1;
		IF @istest = 1 
		BEGIN
			SET @ret_err_msg = @ret_err_msg + 'Skipped Insert transactionrecord due to test. ';
		END
		ELSE BEGIN
			SET @ret_err_msg = @ret_err_msg + 'Skipped Insert transactionrecord due to duplicate. ';
		END

	END 

    -- Insert statements for procedure here
	SELECT @ret_is_success [is_success], @ret_err_msg [err_msg]
END
GO
