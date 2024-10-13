namespace OE.Tudasbazis.Application.Exceptions
{
	public class BusinessLogicException : Exception
	{
		public int StatusCode { get; set; }

		public BusinessLogicException(string message, int statusCode = 400) : base(message)
		{
			StatusCode = statusCode;
		}
	}
}
