using System;

namespace System.Runtime
{
	// Token: 0x02000009 RID: 9
	internal class AsyncEventArgs<TArgument, TResult> : AsyncEventArgs<TArgument>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002681 File Offset: 0x00000881
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002689 File Offset: 0x00000889
		public TResult Result { get; set; }
	}
}
