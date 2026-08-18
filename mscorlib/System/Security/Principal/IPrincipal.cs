using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x020004C6 RID: 1222
	[ComVisible(true)]
	public interface IPrincipal
	{
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060030ED RID: 12525
		IIdentity Identity { get; }

		// Token: 0x060030EE RID: 12526
		bool IsInRole(string role);
	}
}
