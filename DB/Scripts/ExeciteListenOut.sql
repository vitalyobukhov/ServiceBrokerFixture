-- 05/28/2013
-- SP_ListenOut execution script
-- tries to dequeue one message from OutQueue using sp

USE [ServiceBrokerFixture]
GO

DECLARE @RC int
DECLARE @timeout int = 5000
DECLARE @messageBody xml

EXECUTE 
	@RC = [dbo].[ListenOut] 
	@timeout,
	@messageBody OUTPUT

SELECT
	@RC,
	@messageBody
GO

