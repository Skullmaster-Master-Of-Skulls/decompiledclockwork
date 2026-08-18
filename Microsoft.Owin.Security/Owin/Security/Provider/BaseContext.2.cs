using System;

namespace Microsoft.Owin.Security.Provider
{
	// Token: 0x02000023 RID: 35
	public abstract class BaseContext<TOptions>
	{
		// Token: 0x0600008D RID: 141 RVA: 0x000041E2 File Offset: 0x000023E2
		protected BaseContext(IOwinContext context, TOptions options)
		{
			this.OwinContext = context;
			this.Options = options;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000041F8 File Offset: 0x000023F8
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00004200 File Offset: 0x00002400
		public IOwinContext OwinContext { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004209 File Offset: 0x00002409
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00004211 File Offset: 0x00002411
		public TOptions Options { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000421A File Offset: 0x0000241A
		public IOwinRequest Request
		{
			get
			{
				return this.OwinContext.Request;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004227 File Offset: 0x00002427
		public IOwinResponse Response
		{
			get
			{
				return this.OwinContext.Response;
			}
		}
	}
}
