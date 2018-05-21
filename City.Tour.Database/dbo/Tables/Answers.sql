CREATE TABLE [dbo].[Answers] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [CreateTime]      DATETIME         NOT NULL,
    [PuzzleId]        UNIQUEIDENTIFIER NOT NULL,
    [ModifyTime]      DATETIME         NOT NULL,
    [IsDelete]        BIT              NOT NULL,
    [Content]         NVARCHAR (MAX)   NOT NULL,
    [IsCorrectAnswer] BIT              NOT NULL,
    [ReplyMessage]    NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Answers_Puzzles] FOREIGN KEY ([PuzzleId]) REFERENCES [dbo].[Puzzles] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Answers_Puzzles]
    ON [dbo].[Answers]([PuzzleId] ASC);

