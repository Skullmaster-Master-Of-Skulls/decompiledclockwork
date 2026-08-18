using System;

namespace System.Runtime
{
	// Token: 0x02000008 RID: 8
	internal class AsyncEventArgs<TArgument> : AsyncEventArgs
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002657 File Offset: 0x00000857
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000265F File Offset: 0x0000085F
		public TArgument Arguments { get; private set; }

		// Token: 0x06000024 RID: 36 RVA: 0x00002668 File Offset: 0x00000868
		public virtual void Set(AsyncEventArgsCallback callback, TArgument arguments, object state)
		{
			base.SetAsyncState(callback, state);
			this.Arguments = arguments;
		}
	}
}
