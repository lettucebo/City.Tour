CREATE TABLE [dbo].[Teams] (
    [Id]                    UNIQUEIDENTIFIER CONSTRAINT [DF_Teams_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name]                  NVARCHAR (500)   NOT NULL,
    [CreateTime]            DATETIME         CONSTRAINT [DF_Teams_CreateTime] DEFAULT (getdate()) NOT NULL,
    [InviteCode]            NVARCHAR (50)    NOT NULL,
    [TourId]                UNIQUEIDENTIFIER NOT NULL,
    [CurrentTourPuzzleSort] INT              CONSTRAINT [DF_Teams_CurrentPuzzleNum] DEFAULT ((1)) NOT NULL,
    [IsComplete]            BIT              CONSTRAINT [DF_Teams_IsComplete] DEFAULT ((0)) NOT NULL,
    [CurrentPuzzleId]       UNIQUEIDENTIFIER NULL,
    [CurrentTourPuzzleId]   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Teams_Tours] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tours] ([Id])
);






GO
CREATE NONCLUSTERED INDEX [IX_FK_Teams_Tours]
    ON [dbo].[Teams]([TourId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否完成', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Teams', @level2type = N'COLUMN', @level2name = N'IsComplete';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'團隊列表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Teams', @level2type = N'COLUMN', @level2name = N'Id';

