-- 05/28/2013
-- service broker activation script
-- enables service broker (note: force mode - all db connections will be force closed)

USE [ServiceBrokerFixture]
GO

ALTER DATABASE 
	[ServiceBrokerFixture]
SET 
	ENABLE_BROKER WITH ROLLBACK IMMEDIATE
GO
