using OE.Tudasbazis.Application.DTOs.Service;
using OE.Tudasbazis.Application.Exceptions;

namespace OE.Tudasbazis.Application.Services
{
	public interface IElasticService
	{
		/// <summary>
		///		Indexes the embedding of a document asynchronously.
		/// </summary>
		/// <param name="document">The ElasticDocument to be indexed.</param>
		/// <exception cref="BusinessLogicException">Thrown when an error occurs during indexing.</exception>
		Task IndexEmbeddingAsync(ElasticDocument document);

        /// <summary>
		///		Searches for the top K similar documents to the given query embedding.
        /// </summary>
        /// <param name="queryEmbedding">The query embedding to search for similar documents.</param>
        /// <param name="topK">The number of similar documents to retrieve (default is 5).</param>
		/// <exception cref="BusinessLogicException">Thrown when an error occurs during searching.</exception>
        /// <returns>A list of ElasticDocuments representing the search results.</returns>
        Task<List<ElasticDocument>> SearchAsync(float[] queryEmbedding, int topK = 5);
	}
}
