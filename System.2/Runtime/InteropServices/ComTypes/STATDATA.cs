using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020003E5 RID: 997
	[__DynamicallyInvokable]
	public struct STATDATA
	{
		// Token: 0x0400209E RID: 8350
		[__DynamicallyInvokable]
		public FORMATETC formatetc;

		// Token: 0x0400209F RID: 8351
		[__DynamicallyInvokable]
		public ADVF advf;

		// Token: 0x040020A0 RID: 8352
		[__DynamicallyInvokable]
		public IAdviseSink advSink;

		// Token: 0x040020A1 RID: 8353
		[__DynamicallyInvokable]
		public int connection;
	}
}
