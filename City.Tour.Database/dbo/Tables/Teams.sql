CREATE TABLE [dbo].[Teams] (
    [Id]               UNIQUEIDENTIFIER CONSTRAINT [DF_Teams_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name]             NVARCHAR (500)   NOT NULL,
    [CreateTime]       DATETIME         CONSTRAINT [DF_Teams_CreateTime] DEFAULT (getdate()) NOT NULL,
    [InviteCode]       NVARCHAR (50)    NOT NULL,
    [TourId]           UNIQUEIDENTIFIER NOT NULL,
    [CurrentPuzzleId]  UNIQUEIDENTIFIER NULL,
    [CurrentPuzzleNum] INT              CONSTRAINT [DF_Teams_CurrentPuzzleNum] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Teams_Tours] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tours] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Teams_Tours]
    ON [dbo].[Teams]([TourId] ASC);

