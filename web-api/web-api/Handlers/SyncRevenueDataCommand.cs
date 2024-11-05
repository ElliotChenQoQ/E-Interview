// CompaniesController.cs
using MediatR;

namespace WebApp.Handlers
{
    public class SyncRevenueDataCommand : IRequest<Unit> { }
}
