using Microsoft.AspNetCore.Mvc;
using Inflow.DataService.DTO;

namespace Inflow.DataService.Controllers.Interfaces
{
    public interface IDataController
    {
        Task<IActionResult> Select(DataRequestBody dataRequestBody);

        Task<IActionResult> Update(DataRequestBody dataRequestBody);

        Task<IActionResult> Delete(DataRequestBody dataRequestBody);

        Task<IActionResult> Insert(DataRequestBody dataRequestBody);
    }
}
