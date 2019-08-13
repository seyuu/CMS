CREATE TABLE [dbo].[FormItem] (
    [Id]       INT            NOT NULL,
    [Title]    NVARCHAR (255) NULL,
    [Type]     NVARCHAR (255) NULL,
    [Required] BIT            NOT NULL,
    CONSTRAINT [PK_dbo.FormItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.FormItem_dbo.BlockItem_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[BlockItem] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[FormItem]([Id] ASC);

