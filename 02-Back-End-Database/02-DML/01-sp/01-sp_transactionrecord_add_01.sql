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

    -- Insert statements for procedure here
	SELECT 1 [is_success]
END
GO
