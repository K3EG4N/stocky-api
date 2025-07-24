IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF SCHEMA_ID(N'usr') IS NULL EXEC(N'CREATE SCHEMA [usr];');

CREATE TABLE [usr].[Roles] (
    [RoleId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleId])
);

CREATE TABLE [usr].[Users] (
    [UserId] uniqueidentifier NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
);

CREATE TABLE [usr].[UserRole] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRole_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [usr].[Roles] ([RoleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [usr].[Users] ([UserId]) ON DELETE CASCADE
);

CREATE INDEX [IX_UserRole_RoleId] ON [usr].[UserRole] ([RoleId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250724023228_stocky-migration-v1', N'9.0.7');

ALTER TABLE [usr].[UserRole] DROP CONSTRAINT [FK_UserRole_Roles_RoleId];

ALTER TABLE [usr].[UserRole] DROP CONSTRAINT [FK_UserRole_Users_UserId];

ALTER TABLE [usr].[UserRole] DROP CONSTRAINT [PK_UserRole];

IF SCHEMA_ID(N'per') IS NULL EXEC(N'CREATE SCHEMA [per];');

EXEC sp_rename N'[usr].[UserRole]', N'UserRoles', 'OBJECT';

EXEC sp_rename N'[usr].[UserRoles].[IX_UserRole_RoleId]', N'IX_UserRoles_RoleId', 'INDEX';

ALTER TABLE [usr].[UserRoles] ADD CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]);

CREATE TABLE [per].[PersonTypes] (
    [PersonTypeId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PersonTypes] PRIMARY KEY ([PersonTypeId])
);

CREATE TABLE [per].[Person] (
    [PersonId] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [PersonTypeId] uniqueidentifier NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY ([PersonId]),
    CONSTRAINT [FK_Person_PersonTypes_PersonTypeId] FOREIGN KEY ([PersonTypeId]) REFERENCES [per].[PersonTypes] ([PersonTypeId]) ON DELETE CASCADE
);

CREATE INDEX [IX_Person_PersonTypeId] ON [per].[Person] ([PersonTypeId]);

ALTER TABLE [usr].[UserRoles] ADD CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [usr].[Roles] ([RoleId]) ON DELETE CASCADE;

ALTER TABLE [usr].[UserRoles] ADD CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [usr].[Users] ([UserId]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250724044104_stocky-migration-v2', N'9.0.7');

COMMIT;
GO

