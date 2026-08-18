using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200004B RID: 75
	public class RequestHeaderMapping : MediaTypeMapping
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0000A654 File Offset: 0x00008854
		public RequestHeaderMapping(string headerName, string headerValue, StringComparison valueComparison, bool isValueSubstring, string mediaType) : base(mediaType)
		{
			this.Initialize(headerName, headerValue, valueComparison, isValueSubstring);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000A669 File Offset: 0x00008869
		public RequestHeaderMapping(string headerName, string headerValue, StringComparison valueComparison, bool isValueSubstring, MediaTypeHeaderValue mediaType) : base(mediaType)
		{
			this.Initialize(headerName, headerValue, valueComparison, isValueSubstring);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000A67E File Offset: 0x0000887E
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000A686 File Offset: 0x00008886
		public string HeaderName { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000A68F File Offset: 0x0000888F
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000A697 File Offset: 0x00008897
		public string HeaderValue { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000A6A0 File Offset: 0x000088A0
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000A6A8 File Offset: 0x000088A8
		public StringComparison HeaderValueComparison { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000A6B1 File Offset: 0x000088B1
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000A6B9 File Offset: 0x000088B9
		public bool IsValueSubstring { get; private set; }

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A6C2 File Offset: 0x000088C2
		public override double TryMatchMediaType(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return RequestHeaderMapping.MatchHeaderValue(request, this.HeaderName, this.HeaderValue, this.HeaderValueComparison, this.IsValueSubstring);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A6F0 File Offset: 0x000088F0
		private static double MatchHeaderValue(HttpRequestMessage request, string headerName, string headerValue, StringComparison valueComparison, bool isValueSubstring)
		{
			IEnumerable<string> enumerable;
			if (request.Headers.TryGetValues(headerName, out enumerable))
			{
				foreach (string text in enumerable)
				{
					if (isValueSubstring)
					{
						if (text.IndexOf(headerValue, valueComparison) != -1)
						{
							return 1.0;
						}
					}
					else if (text.Equals(headerValue, valueComparison))
					{
						return 1.0;
					}
				}
			}
			return 0.0;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A780 File Offset: 0x00008980
		private void Initialize(string headerName, string headerValue, StringComparison valueComparison, bool isValueSubstring)
		{
			if (string.IsNullOrWhiteSpace(headerName))
			{
				throw Error.ArgumentNull("headerName");
			}
			if (string.IsNullOrWhiteSpace(headerValue))
			{
				throw Error.ArgumentNull("headerValue");
			}
			StringComparisonHelper.Validate(valueComparison, "valueComparison");
			this.HeaderName = headerName;
			this.HeaderValue = headerValue;
			this.HeaderValueComparison = valueComparison;
			this.IsValueSubstring = isValueSubstring;
		}
	}
}
