using System;
using System.Runtime.InteropServices;
using System.Web.Hosting;

namespace System.Web
{
	// Token: 0x020000D5 RID: 213
	internal sealed class ApplicationImpersonationContext : ImpersonationContext
	{
		// Token: 0x06000DFE RID: 3582 RVA: 0x00027A75 File Offset: 0x00025C75
		internal ApplicationImpersonationContext()
		{
			base.ImpersonateToken(new HandleRef(this, HostingEnvironment.ApplicationIdentityToken));
		}
	}
}
