CREATE TABLE [dbo].[CompanyRevenue] (
    [CompanyID]                 INT             NOT NULL,
    [YearMonth]                 CHAR (6)        NOT NULL,
    [ReportDate]                CHAR (8)        NULL,
    [CurrentMonthRevenue]       DECIMAL (18, 4) NULL,
    [LastMonthRevenue]          DECIMAL (18, 4) NULL,
    [LastYearRevenue]           DECIMAL (18, 4) NULL,
    [MonthGrowth]               DECIMAL (18, 4) NULL,
    [YearGrowth]                DECIMAL (18, 4) NULL,
    [CumulativeMonthRevenue]    DECIMAL (18, 4) NULL,
    [CumulativeLastYearRevenue] DECIMAL (18, 4) NULL,
    [CumulativeGrowth]          DECIMAL (18, 4) NULL,
    [Remarks]                   VARCHAR (100)   NULL,
    PRIMARY KEY CLUSTERED ([CompanyID] ASC, [YearMonth] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID]) ON DELETE CASCADE
);

