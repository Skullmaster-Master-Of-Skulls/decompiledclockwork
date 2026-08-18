using System;
using System.Net.Http;

namespace System.Web.Http.Hosting
{
	// Token: 0x020000B5 RID: 181
	public interface IHostBufferPolicySelector
	{
		// Token: 0x06000412 RID: 1042
		bool UseBufferedInputStream(object hostContext);

		// Token: 0x06000413 RID: 1043
		bool UseBufferedOutputStream(HttpResponseMessage response);
	}
}
