CREATE TABLE [dbo].[Users]
(
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Username]   NVARCHAR (MAX) NULL,
    [Password] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
)
