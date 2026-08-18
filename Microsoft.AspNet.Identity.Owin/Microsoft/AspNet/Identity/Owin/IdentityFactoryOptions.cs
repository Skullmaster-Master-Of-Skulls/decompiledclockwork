using System;
using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.AspNet.Identity.Owin
{
	// Token: 0x02000004 RID: 4
	public class IdentityFactoryOptions<T> where T : IDisposable
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000267A File Offset: 0x0000087A
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002682 File Offset: 0x00000882
		public IDataProtectionProvider DataProtectionProvider { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000268B File Offset: 0x0000088B
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002693 File Offset: 0x00000893
		public IIdentityFactoryProvider<T> Provider { get; set; }
	}
}
