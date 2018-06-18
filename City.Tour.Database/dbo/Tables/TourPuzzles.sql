CREATE TABLE [dbo].[TourPuzzles] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_TourPuzzles_Id] DEFAULT (newsequentialid()) NOT NULL,
    [PuzzleId]   UNIQUEIDENTIFIER NOT NULL,
    [TourId]     UNIQUEIDENTIFIER NOT NULL,
    [Sort]       INT              NOT NULL,
    [CreateTime] DATETIME         CONSTRAINT [DF_TourPuzzles_CreateTime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_TourPuzzles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TourPuzzles_Puzzles1] FOREIGN KEY ([PuzzleId]) REFERENCES [dbo].[Puzzles] ([Id]),
    CONSTRAINT [FK_TourPuzzles_Tours1] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tours] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'路線謎題對應表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TourPuzzles', @level2type = N'COLUMN', @level2name = N'Id';

