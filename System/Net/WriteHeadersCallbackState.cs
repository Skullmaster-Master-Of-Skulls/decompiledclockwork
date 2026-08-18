using System;

namespace System.Net
{
	// Token: 0x020004CE RID: 1230
	internal struct WriteHeadersCallbackState
	{
		// Token: 0x06002615 RID: 9749 RVA: 0x00099C18 File Offset: 0x00098C18
		internal WriteHeadersCallbackState(HttpWebRequest request, ConnectStream stream)
		{
			this.request = request;
			this.stream = stream;
		}

		// Token: 0x040025CA RID: 9674
		internal HttpWebRequest request;

		// Token: 0x040025CB RID: 9675
		internal ConnectStream stream;
	}
}
