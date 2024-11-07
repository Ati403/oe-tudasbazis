namespace OE.Tudasbazis.Application.Services
{
	public interface IPdfProcessorService
	{
        /// <summary>
        ///		Processes a PDF file from the provided stream and extracts text content in chunks suitable for uploading to a vector database.
        /// </summary>
        /// <param name="pdfStream">The stream containing the PDF file to be processed.</param>
        /// <returns>A list of strings, each representing a chunk of the PDF's text content.</returns>
        public List<string> ProcessPdf(Stream pdfStream);
	}
}
