using System;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace System.Web.Http.Tracing
{
	// Token: 0x02000151 RID: 337
	internal interface IFormatterTracer
	{
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600086F RID: 2159
		HttpRequestMessage Request { get; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000870 RID: 2160
		MediaTypeFormatter InnerFormatter { get; }
	}
}
