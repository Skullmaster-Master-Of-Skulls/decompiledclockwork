using System;
using System.Collections.Generic;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000040 RID: 64
	public interface IContentNegotiator
	{
		// Token: 0x0600024C RID: 588
		ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters);
	}
}
