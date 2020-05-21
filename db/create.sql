IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ColumnModels]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ColumnModels](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Position] [int] NOT NULL,
CONSTRAINT [pk_ColumnModels_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaskModels]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TaskModels](
	[ID] [uniqueidentifier] NOT NULL,
	[ColumnID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Position] [int] NOT NULL,
CONSTRAINT [pk_TaskModels_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_TaskModels_ColumnModels_ColumnID]') AND parent_object_id = OBJECT_ID(N'[dbo].[TaskModels]'))
ALTER TABLE [dbo].[TaskModels]  WITH CHECK ADD CONSTRAINT [fk_TaskModels_ColumnModels_ColumnID] FOREIGN KEY([ColumnID])
REFERENCES [dbo].[ColumnModels] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_TaskModels_ColumnModels_ColumnID]') AND parent_object_id = OBJECT_ID(N'[dbo].[TaskModels]'))
ALTER TABLE [dbo].[TaskModels] CHECK CONSTRAINT [fk_TaskModels_ColumnModels_ColumnID]
GO
