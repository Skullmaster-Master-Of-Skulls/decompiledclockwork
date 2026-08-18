using System;

namespace Microsoft.Owin.Security.Provider
{
	// Token: 0x02000015 RID: 21
	public abstract class BaseContext
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002A3D File Offset: 0x00000C3D
		protected BaseContext(IOwinContext context)
		{
			this.OwinContext = context;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002A4C File Offset: 0x00000C4C
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002A54 File Offset: 0x00000C54
		public IOwinContext OwinContext { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002A5D File Offset: 0x00000C5D
		public IOwinRequest Request
		{
			get
			{
				return this.OwinContext.Request;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A6A File Offset: 0x00000C6A
		public IOwinResponse Response
		{
			get
			{
				return this.OwinContext.Response;
			}
		}
	}
}
