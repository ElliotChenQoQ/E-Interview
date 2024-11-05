
CREATE PROCEDURE InsertRevenue
    @CompanyID INT,
	@CompanyName VARCHAR(100),
	@Industry VARCHAR(100),
    @YearMonth CHAR(6),
    @ReportDate CHAR(8),
    @CurrentMonthRevenue DECIMAL(18, 4),
    @LastMonthRevenue DECIMAL(18, 4),
    @LastYearRevenue DECIMAL(18, 4),
    @MonthGrowth DECIMAL(18, 4),
    @YearGrowth DECIMAL(18, 4),
    @CumulativeMonthRevenue DECIMAL(18, 4),
    @CumulativeLastYearRevenue DECIMAL(18, 4),
    @CumulativeGrowth DECIMAL(18, 4),
    @Remarks VARCHAR(100)
AS
BEGIN
    BEGIN TRANSACTION;

    -- 確保 Company 表中存在該 CompanyID
    IF NOT EXISTS (SELECT 1 FROM Company WHERE CompanyID = @CompanyID)
    BEGIN
        INSERT INTO Company (CompanyID, CompanyName, Industry)
        VALUES (@CompanyID, @CompanyName, @Industry);
    END

    -- 更新或插入 CompanyRevenue
    IF EXISTS (SELECT 1 FROM CompanyRevenue WHERE CompanyID = @CompanyID AND YearMonth = @YearMonth)
    BEGIN
        UPDATE CompanyRevenue
        SET ReportDate = @ReportDate,
            CurrentMonthRevenue = @CurrentMonthRevenue,
            LastMonthRevenue = @LastMonthRevenue,
            LastYearRevenue = @LastYearRevenue,
            MonthGrowth = @MonthGrowth,
            YearGrowth = @YearGrowth,
            CumulativeMonthRevenue = @CumulativeMonthRevenue,
            CumulativeLastYearRevenue = @CumulativeLastYearRevenue,
            CumulativeGrowth = @CumulativeGrowth,
            Remarks = @Remarks
        WHERE CompanyID = @CompanyID AND YearMonth = @YearMonth;
    END
    ELSE
    BEGIN
        INSERT INTO CompanyRevenue (CompanyID, YearMonth, ReportDate, CurrentMonthRevenue, LastMonthRevenue, LastYearRevenue, MonthGrowth, YearGrowth, CumulativeMonthRevenue, CumulativeLastYearRevenue, CumulativeGrowth, Remarks)
        VALUES (@CompanyID, @YearMonth, @ReportDate, @CurrentMonthRevenue, @LastMonthRevenue, @LastYearRevenue, @MonthGrowth, @YearGrowth, @CumulativeMonthRevenue, @CumulativeLastYearRevenue, @CumulativeGrowth, @Remarks);
    END

    COMMIT TRANSACTION;
END;