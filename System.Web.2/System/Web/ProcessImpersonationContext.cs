using System;
using System.Runtime.InteropServices;

namespace System.Web
{
	// Token: 0x020000D4 RID: 212
	internal sealed class ProcessImpersonationContext : ImpersonationContext
	{
		// Token: 0x06000DFD RID: 3581 RVA: 0x00027A5C File Offset: 0x00025C5C
		internal ProcessImpersonationContext()
		{
			base.ImpersonateToken(new HandleRef(this, IntPtr.Zero));
		}
	}
}
