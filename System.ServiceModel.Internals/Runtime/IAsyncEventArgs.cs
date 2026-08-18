using System;

namespace System.Runtime
{
	// Token: 0x0200001C RID: 28
	internal interface IAsyncEventArgs
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C4 RID: 196
		object AsyncState { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C5 RID: 197
		Exception Exception { get; }
	}
}
