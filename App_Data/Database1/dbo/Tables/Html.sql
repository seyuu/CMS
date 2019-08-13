CREATE TABLE [dbo].[Html] (
    [Id]      INT            NOT NULL,
    [Content] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Html] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Html_dbo.Block_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Block] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Html]([Id] ASC);

