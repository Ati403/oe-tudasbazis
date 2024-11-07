using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OE.Tudasbazis.Application.DTOs.Requests;
using OE.Tudasbazis.Application.Enums;
using OE.Tudasbazis.Application.Services;

namespace OE.Tudasbazis.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = nameof(UserRole.Admin))]
	public class DataMaintainController : ControllerBase
	{
		private readonly IDataMaintainService _dataMaintainService;

		public DataMaintainController(IDataMaintainService dataMaintainService)
		{
			_dataMaintainService = dataMaintainService;
		}

		/// <summary>
		///		Uploads a string to the vector database.
		/// </summary>
		/// <param name="dataMaintainDto">Contains the text to be uploaded to the vector database.</param>
		[HttpPost]
		[Route("upload/text")]
		public async Task<IActionResult> UploadStringAsync([FromBody] DataMaintainDto dataMaintainDto)
		{
			await _dataMaintainService.UploadStringToVectorDatabaseAsync(dataMaintainDto.Text);

			return Created();
		}

		/// <summary>
		///		Uploads a PDF file to the vector database.
		/// </summary>
		/// <param name="file">The PDF file to be uploaded to the vector database.</param>
		[HttpPost]
		[Route("upload/pdf")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> UploadPdfAsync(IFormFile file)
		{
			await _dataMaintainService.UploadPdfToVectoDatabaseAsync(file.FileName, file.OpenReadStream());

			return Created();
		}
	}
}
