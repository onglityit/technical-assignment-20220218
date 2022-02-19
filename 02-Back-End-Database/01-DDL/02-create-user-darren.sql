USE [master]
GO
CREATE LOGIN [darren] WITH PASSWORD=N'darren', DEFAULT_DATABASE=[InternationalTransaction], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
USE [InternationalTransaction]
GO
CREATE USER [darren] FOR LOGIN [darren]
GO
USE [InternationalTransaction]
GO
ALTER USER [darren] WITH DEFAULT_SCHEMA=[darren]
GO
USE [InternationalTransaction]
GO
CREATE SCHEMA [darren] AUTHORIZATION [darren]
GO



USE [master]
GO
EXEC xp_instance_regwrite N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'LoginMode', REG_DWORD, 2
GO
