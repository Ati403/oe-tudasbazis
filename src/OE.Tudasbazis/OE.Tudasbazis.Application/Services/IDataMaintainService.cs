using OE.Tudasbazis.Application.Exceptions;

namespace OE.Tudasbazis.Application.Services
{
	public interface IDataMaintainService
	{
		/// <summary>
		///		Genereate vector embedding from the given text and upload it to the vector database
		/// </summary>
		/// <param name="text">Data intended to expand the knowledge base.</param>
		public Task UploadStringToVectorDatabaseAsync(string text);

		/// <summary>
		///		Generate vector embeddings from the given PDF stream and upload them to the vector database
		/// </summary>
		/// <param name="fileName">Name of the received file.</param>
		/// <param name="pdfStream">PDF stream intended to expand the knowledge base.</param>
		/// <exception cref="BusinessLogicException">Thrown when the file extension is not .pdf</exception>
		public Task UploadPdfToVectoDatabaseAsync(string fileName, Stream pdfStream);
	}
}
