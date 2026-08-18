using System;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x020006B1 RID: 1713
	internal class MultiAsyncResult : LazyAsyncResult
	{
		// Token: 0x060034F4 RID: 13556 RVA: 0x000E0FA7 File Offset: 0x000DFFA7
		internal MultiAsyncResult(object context, AsyncCallback callback, object state) : base(context, state, callback)
		{
			this.context = context;
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x000E0FB9 File Offset: 0x000DFFB9
		internal object Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000E0FC1 File Offset: 0x000DFFC1
		internal void Enter()
		{
			this.Increment();
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000E0FC9 File Offset: 0x000DFFC9
		internal void Leave()
		{
			this.Decrement();
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000E0FD1 File Offset: 0x000DFFD1
		internal void Leave(object result)
		{
			base.Result = result;
			this.Decrement();
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000E0FE0 File Offset: 0x000DFFE0
		private void Decrement()
		{
			if (Interlocked.Decrement(ref this.outstanding) == -1)
			{
				base.InvokeCallback(base.Result);
			}
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000E0FFC File Offset: 0x000DFFFC
		private void Increment()
		{
			Interlocked.Increment(ref this.outstanding);
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000E100A File Offset: 0x000E000A
		internal void CompleteSequence()
		{
			this.Decrement();
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000E1014 File Offset: 0x000E0014
		internal static object End(IAsyncResult result)
		{
			MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result;
			multiAsyncResult.InternalWaitForCompletion();
			return multiAsyncResult.Result;
		}

		// Token: 0x040030A0 RID: 12448
		private int outstanding;

		// Token: 0x040030A1 RID: 12449
		private object context;
	}
}
