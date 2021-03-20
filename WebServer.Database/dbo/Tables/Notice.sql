CREATE TABLE [dbo].[Notice] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Title]   NVARCHAR (MAX) NULL,
    [Content] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Notice] PRIMARY KEY CLUSTERED ([Id] ASC)
);

