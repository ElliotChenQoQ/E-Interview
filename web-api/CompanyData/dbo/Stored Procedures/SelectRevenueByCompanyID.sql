
CREATE PROCEDURE SelectRevenueByCompanyID
    @CompanyID INT = NULL
AS
BEGIN
    SELECT C.CompanyID, C.CompanyName, C.Industry, YearMonth, ReportDate, CurrentMonthRevenue, LastMonthRevenue, LastYearRevenue, MonthGrowth, YearGrowth, CumulativeMonthRevenue, CumulativeLastYearRevenue, CumulativeGrowth, Remarks
    FROM CompanyRevenue as r
        JOIN Company as c ON c.CompanyID = r.CompanyID
    WHERE @CompanyID IS NULL OR c.CompanyID = @CompanyID
    ORDER BY YearMonth;
END;
