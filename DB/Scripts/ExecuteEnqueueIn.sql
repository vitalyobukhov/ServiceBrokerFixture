-- 05/28/2013
-- SP_EnqueueIn execution script
-- enqueues one sample message into InQueue using sp

USE [ServiceBrokerFixture]
GO

DECLARE @RC int
DECLARE @messageBody xml = 
	CAST(
	N'<InMessage>' +  
		N'<Id>' +  CAST(NEWID() AS nvarchar(max)) + N'</Id>' +
		N'<Payload>' + REPLACE(CAST(NEWID() AS nvarchar(max)), N'-', N'') + '</Payload>' +
		N'<Produced>' + CAST(0 AS nvarchar(max)) + N'</Produced>' +
	N'</InMessage>'
	AS xml)

EXECUTE 
	@RC = [dbo].[EnqueueIn] 
	@messageBody

SELECT @RC
GO


