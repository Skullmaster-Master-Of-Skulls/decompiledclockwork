using System;

namespace System.Net
{
	// Token: 0x020001A5 RID: 421
	internal struct WriteHeadersCallbackState
	{
		// Token: 0x0600105E RID: 4190 RVA: 0x00057760 File Offset: 0x00055960
		internal WriteHeadersCallbackState(HttpWebRequest request, ConnectStream stream)
		{
			this.request = request;
			this.stream = stream;
		}

		// Token: 0x04001381 RID: 4993
		internal HttpWebRequest request;

		// Token: 0x04001382 RID: 4994
		internal ConnectStream stream;
	}
}
