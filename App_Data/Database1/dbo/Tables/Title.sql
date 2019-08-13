CREATE TABLE [dbo].[Title] (
    [Id]      INT            NOT NULL,
    [Content] NVARCHAR (255) NULL,
    CONSTRAINT [PK_dbo.Title] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Title_dbo.Block_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Block] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Title]([Id] ASC);

