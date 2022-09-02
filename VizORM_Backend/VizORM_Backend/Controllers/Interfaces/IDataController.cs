using Microsoft.AspNetCore.Mvc;
using VizORM_Backend.Bodies;

namespace VizORM_Backend.Controllers.Interfaces
{
    public interface IDataController
    {
        Task<IActionResult> Select(DataRequestBody dataRequestBody);

        Task<IActionResult> Update(DataRequestBody dataRequestBody);

        Task<IActionResult> Delete(DataRequestBody dataRequestBody);

        Task<IActionResult> Insert(DataRequestBody dataRequestBody);
    }
}
