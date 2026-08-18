using System;

namespace Microsoft.Owin.Security.Provider
{
	// Token: 0x02000032 RID: 50
	public abstract class EndpointContext : BaseContext
	{
		// Token: 0x060000CF RID: 207 RVA: 0x0000478B File Offset: 0x0000298B
		protected EndpointContext(IOwinContext context) : base(context)
		{
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004794 File Offset: 0x00002994
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000479C File Offset: 0x0000299C
		public bool IsRequestCompleted { get; private set; }

		// Token: 0x060000D2 RID: 210 RVA: 0x000047A5 File Offset: 0x000029A5
		public void RequestCompleted()
		{
			this.IsRequestCompleted = true;
		}
	}
}
