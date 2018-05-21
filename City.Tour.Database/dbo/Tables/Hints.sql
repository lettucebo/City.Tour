CREATE TABLE [dbo].[Hints] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [CreateTime] DATETIME         NOT NULL,
    [PuzzleId]   UNIQUEIDENTIFIER NOT NULL,
    [IsDelete]   BIT              NOT NULL,
    [ModifyTime] DATETIME         NOT NULL,
    [Content]    NVARCHAR (MAX)   NOT NULL,
    [Sort]       INT              NOT NULL,
    CONSTRAINT [PK_Hints] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Hints_Puzzles] FOREIGN KEY ([PuzzleId]) REFERENCES [dbo].[Puzzles] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Hints_Puzzles]
    ON [dbo].[Hints]([PuzzleId] ASC);

