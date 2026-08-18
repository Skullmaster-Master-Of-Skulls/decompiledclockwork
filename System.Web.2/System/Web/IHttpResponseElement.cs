using System;

namespace System.Web
{
	// Token: 0x020000C1 RID: 193
	internal interface IHttpResponseElement
	{
		// Token: 0x06000D58 RID: 3416
		long GetSize();

		// Token: 0x06000D59 RID: 3417
		byte[] GetBytes();

		// Token: 0x06000D5A RID: 3418
		void Send(HttpWorkerRequest wr);
	}
}
