CREATE TABLE [dbo].[Section] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [No]        INT            NOT NULL,
    [Title]     NVARCHAR (MAX) NULL,
    [Invert]    BIT            NOT NULL,
    [Full]      BIT            NOT NULL,
    [Container] NVARCHAR (MAX) NULL,
    [PageId]    INT            NOT NULL,
    CONSTRAINT [PK_dbo.Section] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Section_dbo.Page_PageId] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PageId]
    ON [dbo].[Section]([PageId] ASC);

