using System;
using System.Net;
using System.Runtime.Serialization;
using Bmbsqd.SaferPay.Models;

namespace Bmbsqd.SaferPay
{
	public class SaferPayException : Exception
	{
		public SaferPayException( HttpStatusCode httpStatusCode, ErrorResponse errorResponse ) : base( $"{httpStatusCode}: {errorResponse.ErrorName}: {errorResponse.ErrorMessage}: {string.Join( ", ", errorResponse.ErrorDetail ?? Array.Empty<string>() )}" )
		{
			HttpStatusCode = httpStatusCode;
			ErrorResponse = errorResponse;
		}

		public HttpStatusCode HttpStatusCode { get; }
		public ErrorResponse ErrorResponse { get; }

		protected SaferPayException( SerializationInfo info, StreamingContext context ) : base( info, context ) { }

		public override string ToString() => base.ToString() +
			Environment.NewLine + "=======" + Environment.NewLine +
			ErrorResponse;
	}
}