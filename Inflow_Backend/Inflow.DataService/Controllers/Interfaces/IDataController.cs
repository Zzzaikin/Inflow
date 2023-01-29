using Microsoft.AspNetCore.Mvc;
using Inflow.DataService.DTO;

namespace Inflow.DataService.Controllers.Interfaces
{
    public interface IDataController
    {
        Task<IActionResult> Select(SelectDataRequestBody dataRequestBody);

        Task<IActionResult> Update(UpdateDataRequestBody dataRequestBody);

        Task<IActionResult> Delete(DeleteDataRequestBody dataRequestBody);

        Task<IActionResult> Insert(InsertDataRequestBody dataRequestBody);
    }
}
