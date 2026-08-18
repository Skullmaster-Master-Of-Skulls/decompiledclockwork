using System;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000046 RID: 70
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class MediaTypeFormatterExtensions
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x0000A164 File Offset: 0x00008364
		public static void AddQueryStringMapping(this MediaTypeFormatter formatter, string queryStringParameterName, string queryStringParameterValue, MediaTypeHeaderValue mediaType)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			QueryStringMapping item = new QueryStringMapping(queryStringParameterName, queryStringParameterValue, mediaType);
			formatter.MediaTypeMappings.Add(item);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000A194 File Offset: 0x00008394
		public static void AddQueryStringMapping(this MediaTypeFormatter formatter, string queryStringParameterName, string queryStringParameterValue, string mediaType)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			QueryStringMapping item = new QueryStringMapping(queryStringParameterName, queryStringParameterValue, mediaType);
			formatter.MediaTypeMappings.Add(item);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A1C4 File Offset: 0x000083C4
		public static void AddRequestHeaderMapping(this MediaTypeFormatter formatter, string headerName, string headerValue, StringComparison valueComparison, bool isValueSubstring, MediaTypeHeaderValue mediaType)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			RequestHeaderMapping item = new RequestHeaderMapping(headerName, headerValue, valueComparison, isValueSubstring, mediaType);
			formatter.MediaTypeMappings.Add(item);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A1F8 File Offset: 0x000083F8
		public static void AddRequestHeaderMapping(this MediaTypeFormatter formatter, string headerName, string headerValue, StringComparison valueComparison, bool isValueSubstring, string mediaType)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			RequestHeaderMapping item = new RequestHeaderMapping(headerName, headerValue, valueComparison, isValueSubstring, mediaType);
			formatter.MediaTypeMappings.Add(item);
		}
	}
}
