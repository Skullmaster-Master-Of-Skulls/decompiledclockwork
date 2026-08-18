using System;

namespace Microsoft.Owin.Security.Provider
{
	// Token: 0x02000031 RID: 49
	public abstract class EndpointContext<TOptions> : BaseContext<TOptions>
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00004767 File Offset: 0x00002967
		protected EndpointContext(IOwinContext context, TOptions options) : base(context, options)
		{
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004771 File Offset: 0x00002971
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004779 File Offset: 0x00002979
		public bool IsRequestCompleted { get; private set; }

		// Token: 0x060000CE RID: 206 RVA: 0x00004782 File Offset: 0x00002982
		public void RequestCompleted()
		{
			this.IsRequestCompleted = true;
		}
	}
}
