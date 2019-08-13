CREATE TABLE [dbo].[MenuItem] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [No]     INT            NOT NULL,
    [Title]  NVARCHAR (MAX) NULL,
    [Blank]  BIT            NOT NULL,
    [Url]    NVARCHAR (MAX) NULL,
    [MenuId] INT            NOT NULL,
    CONSTRAINT [PK_dbo.MenuItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.MenuItem_dbo.Menu_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [dbo].[Menu] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_MenuId]
    ON [dbo].[MenuItem]([MenuId] ASC);

