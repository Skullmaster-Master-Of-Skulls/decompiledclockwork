using System;
using System.ComponentModel;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000038 RID: 56
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpRequestMessageExtensions
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00007C84 File Offset: 0x00005E84
		public static HttpResponseMessage CreateResponse(this HttpRequestMessage request, HttpStatusCode statusCode)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return new HttpResponseMessage
			{
				StatusCode = statusCode,
				RequestMessage = request
			};
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007CB4 File Offset: 0x00005EB4
		public static HttpResponseMessage CreateResponse(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return new HttpResponseMessage
			{
				RequestMessage = request
			};
		}
	}
}
