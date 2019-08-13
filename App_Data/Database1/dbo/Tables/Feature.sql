CREATE TABLE [dbo].[Feature] (
    [Id]          INT             NOT NULL,
    [Title]       NVARCHAR (255)  NULL,
    [Description] NVARCHAR (1000) NULL,
    [Image]       NVARCHAR (255)  NULL,
    [Link]        NVARCHAR (1000) NULL,
    [Icon]        BIT             NOT NULL,
    CONSTRAINT [PK_dbo.Feature] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Feature_dbo.Block_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Block] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Feature]([Id] ASC);

