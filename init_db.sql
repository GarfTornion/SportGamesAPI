USE [<your-database>]
GO

/****** Object:  Table [dbo].[SportGame]    Script Date: 15/01/2023 11.23.59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SportGame](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[team1Name] [nvarchar](50) NULL,
	[team1Score] [int] NULL,
	[team2Name] [nvarchar](50) NULL,
	[team2Score] [int] NULL,
	[startTime] [datetime] NULL,
	[endTime] [datetime] NULL,
	[finished] [bit] NULL,
 CONSTRAINT [PK_SportGame] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SportGame] ADD  CONSTRAINT [DF_SportGame_finished]  DEFAULT ((0)) FOR [finished]
GO
