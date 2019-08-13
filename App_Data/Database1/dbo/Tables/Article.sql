CREATE TABLE [dbo].[Article] (
    [Id]      INT             NOT NULL,
    [Date]    DATETIME        NOT NULL,
    [Title]   NVARCHAR (255)  NOT NULL,
    [Summary] NVARCHAR (1000) NOT NULL,
    [Detail]  NVARCHAR (MAX)  NOT NULL,
    CONSTRAINT [PK_dbo.Article] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Article_dbo.Block_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Block] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Article]([Id] ASC);

