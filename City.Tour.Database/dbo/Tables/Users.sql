CREATE TABLE [dbo].[Users] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_Users_Id] DEFAULT (newsequentialid()) NOT NULL,
    [TeamId]     UNIQUEIDENTIFIER NOT NULL,
    [Name]       NVARCHAR (500)   NOT NULL,
    [CreateTime] DATETIME         CONSTRAINT [DF_Users_CreateTime] DEFAULT (getdate()) NOT NULL,
    [ProfileId]  NVARCHAR (500)   NOT NULL,
    [Picture]    NVARCHAR (MAX)   NOT NULL,
    [Email]      NVARCHAR (500)   NOT NULL,
    [Source]     NVARCHAR (500)   NOT NULL,
    [IsDelete]   BIT              CONSTRAINT [DF_Users_IsDelete] DEFAULT ((0)) NOT NULL,
    [ModifyTime] DATETIME         CONSTRAINT [DF_Users_ModifyTime] DEFAULT (getdate()) NOT NULL,
    [IsAdmin]    BIT              CONSTRAINT [DF_Users_IsAdmin] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Members_Teams] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Members_Teams]
    ON [dbo].[Users]([TeamId] ASC);

