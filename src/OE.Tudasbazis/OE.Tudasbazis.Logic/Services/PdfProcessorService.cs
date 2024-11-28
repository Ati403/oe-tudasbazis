using System.Text;

using OE.Tudasbazis.Application.Services;

using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace OE.Tudasbazis.Logic.Services
{
	public class PdfProcessorService : IPdfProcessorService
	{
		private const int MIN_CHUNK_SIZE = 500;
		private const float HEADER_THRESHOLD = 0.9f;
		private const float FOOTER_THRESHOLD = 0.1f;

		public List<string> ProcessPdf(Stream pdfStream)
		{
			var result = new List<string>();
			var currentText = new StringBuilder();
			bool foundTableOfContents = false;
			int skipPagesCount = 0;

			using var document = PdfDocument.Open(pdfStream);

			int totalPages = document.NumberOfPages;

			for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
			{
				var page = document.GetPage(pageNumber);
				string pageContent = ExtractPageContent(page);

				if (!foundTableOfContents && pageContent.Contains("Tartalomjegyzék"))
				{
					foundTableOfContents = true;
					skipPagesCount = 4;
					continue;
				}

				if (foundTableOfContents && skipPagesCount > 0)
				{
					skipPagesCount--;
					continue;
				}

				currentText.AppendLine(pageContent.Trim());
			}

			result = SplitTextIntoChunks(currentText.ToString());

			return result;
		}

		private string ExtractPageContent(Page page)
		{
			var pageContent = new StringBuilder();

			foreach (var word in page.GetWords())
			{
				if (word.BoundingBox.Bottom > page.Height * HEADER_THRESHOLD
					|| word.BoundingBox.Top < page.Height * FOOTER_THRESHOLD)
				{
					continue;
				}

				pageContent.Append(word.Text + " ");
			}

			return pageContent.ToString();
		}

		private List<string> SplitTextIntoChunks(string text)
		{
			var result = new List<string>();
			string[] sentences = text.Split(['.', '!', '?', ' '], StringSplitOptions.RemoveEmptyEntries);
			var currentChunk = new StringBuilder();

			foreach (string sentence in sentences)
			{
				string trimmedSentence = sentence.Trim()/* + "."*/;

				if (currentChunk.Length + trimmedSentence.Length < MIN_CHUNK_SIZE)
				{
					currentChunk.Append(trimmedSentence + " ");
				}
				else
				{
					currentChunk.Append(trimmedSentence + " ");
					result.Add(currentChunk.ToString().Trim());
					currentChunk.Clear();
				}
			}

			if (currentChunk.Length > 0)
			{
				result.Add(currentChunk.ToString().Trim());
			}

			return result;
		}
	}
}
