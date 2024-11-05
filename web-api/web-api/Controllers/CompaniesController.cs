// CompaniesController.cs
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using WebApp.Handlers;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// �d�ߤ��q�C����~���J���
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet("revenues/{companyId}")]
        public async Task<IActionResult> GetCompanyRevenues(int companyId)
        {
            var query = new GetCompanyRevenuesQuery(companyId);
            var revenues = await _mediator.Send(query);
            if (revenues == null || !revenues.Any())
                return NotFound();

            return Ok(revenues);
        }

        /// <summary>
        /// �d�ߩҦ����q�C����~���J���
        /// </summary>
        /// <returns></returns>
        [HttpGet("revenues")]
        public async Task<IActionResult> GetRevenues()
        {
            var query = new GetCompanyRevenuesQuery(null);
            var revenues = await _mediator.Send(query);
            if (revenues == null || !revenues.Any())
                return NotFound();

            return Ok(revenues);
        }

        /// <summary>
        /// �W�����q��~��ƦP�B
        /// </summary>
        /// <returns></returns>
        [HttpPost("revenues/sync")]
        public async Task<IActionResult> SyncRevenues()
        {
            var command = new SyncRevenueDataCommand();
            await _mediator.Send(command);
            return Ok();
        }
    }
}
