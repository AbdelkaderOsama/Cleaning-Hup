using Cleaning_Hup.Contracts.Response;
using Microsoft.AspNetCore.Mvc;

namespace Cleaning_Hup.Abstraction
{
    public interface IReportService
    {
        Task<SalesReportResponse> GetSalesReportAsync(DateTime fromDate, DateTime toDate);
    }
}
