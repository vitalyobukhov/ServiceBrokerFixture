-- 05/28/2013
-- event notification creation script
-- creates event notification for service broker external activation

USE [ServiceBrokerFixture]
GO

DECLARE @userName sysname = '[YOUR_DOMAIN\YOUR_USERNAME]'
DECLARE @userQueueName sysname = 'InQueue'
DECLARE @notificationQueueName sysname = 'InNotificationQueue'
DECLARE @notificationServiceName sysname = 'InNotificationService'
DECLARE @eventNotificationName sysname = 'InEventNotification'

EXECUTE('
IF (EXISTS(SELECT name FROM sys.event_notifications WHERE name = ''' + @eventNotificationName + '''))
BEGIN
	DROP EVENT NOTIFICATION 
	' + @eventNotificationName + '
	ON QUEUE 
		' + @userQueueName + '
END

GRANT 
	CONNECT 
TO 
	' + @userName + '

GRANT 
	RECEIVE 
ON 
	' + @notificationQueueName + ' 
TO 
	' + @userName + '

GRANT 
	VIEW DEFINITION 
ON 
	SERVICE::' + @notificationServiceName + ' 
TO 
	' + @userName + '

GRANT 
	REFERENCES 
ON 
	SCHEMA::dbo 
TO 
	' + @userName + '

CREATE EVENT NOTIFICATION 
	InEventNotification
ON QUEUE 
	InQueue
FOR QUEUE_ACTIVATION
TO SERVICE 
	''' + @notificationServiceName + ''', ''current database''
')

