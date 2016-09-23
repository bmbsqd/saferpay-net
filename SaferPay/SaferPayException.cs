using System;
using System.Net;
using SaferPay.Models;

namespace SaferPay {
	public class SaferPayException : Exception {
		public SaferPayException( HttpStatusCode httpStatusCode, ErrorResponse errorResponse ) : base( $"{httpStatusCode}: {errorResponse.ErrorName}: {errorResponse.ErrorMessage}: {string.Join( ", ", errorResponse.ErrorDetail ?? Array.Empty<string>() )}" )
		{
			HttpStatusCode = httpStatusCode;
			ErrorResponse = errorResponse;
		}

		public HttpStatusCode HttpStatusCode { get; }
		public ErrorResponse ErrorResponse { get; }

		public override string ToString() =>
			base.ToString() + Environment.NewLine +
			"=======" + Environment.NewLine +
			ErrorResponse;
	}
}