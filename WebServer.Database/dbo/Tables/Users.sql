CREATE TABLE [dbo].[Users]
(
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Email]   NVARCHAR (MAX) NULL,
    [Password] NVARCHAR (MAX) NULL,
    [FirstName] NVARCHAR (MAX) NULL,
    [LastName] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
)
