CREATE TABLE [dbo].[Hints] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_Hints_Id] DEFAULT (newsequentialid()) NOT NULL,
    [CreateTime] DATETIME         CONSTRAINT [DF_Hints_CreateTime] DEFAULT (getdate()) NOT NULL,
    [PuzzleId]   UNIQUEIDENTIFIER NOT NULL,
    [IsDelete]   BIT              CONSTRAINT [DF_Hints_IsDelete] DEFAULT ((0)) NOT NULL,
    [ModifyTime] DATETIME         CONSTRAINT [DF_Hints_ModifyTime] DEFAULT (getdate()) NOT NULL,
    [Content]    NVARCHAR (MAX)   NOT NULL,
    [Sort]       INT              NOT NULL,
    CONSTRAINT [PK_Hints] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Hints_Puzzles] FOREIGN KEY ([PuzzleId]) REFERENCES [dbo].[Puzzles] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_FK_Hints_Puzzles]
    ON [dbo].[Hints]([PuzzleId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提示列表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Hints', @level2type = N'COLUMN', @level2name = N'Id';

