-- 05/28/2013
-- service broker inner queue selection script
-- selects actual service broker messages inner representation

USE [ServiceBrokerFixture]
GO

SELECT
	*
FROM
	sys.transmission_queue