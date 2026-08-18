using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200004F RID: 79
	public class XmlHttpRequestHeaderMapping : RequestHeaderMapping
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0000A8C2 File Offset: 0x00008AC2
		public XmlHttpRequestHeaderMapping() : base("x-requested-with", "XMLHttpRequest", StringComparison.OrdinalIgnoreCase, true, MediaTypeConstants.ApplicationJsonMediaType)
		{
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000A8DC File Offset: 0x00008ADC
		public override double TryMatchMediaType(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (request.Headers.Accept.Count == 0 || (request.Headers.Accept.Count == 1 && request.Headers.Accept.First<MediaTypeWithQualityHeaderValue>().MediaType.Equals("*/*", StringComparison.Ordinal)))
			{
				return base.TryMatchMediaType(request);
			}
			return 0.0;
		}
	}
}
