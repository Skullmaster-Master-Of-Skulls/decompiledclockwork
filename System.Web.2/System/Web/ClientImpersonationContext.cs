using System;
using System.Runtime.InteropServices;
using System.Web.Hosting;

namespace System.Web
{
	// Token: 0x020000D6 RID: 214
	internal sealed class ClientImpersonationContext : ImpersonationContext
	{
		// Token: 0x06000DFF RID: 3583 RVA: 0x00027A8E File Offset: 0x00025C8E
		internal ClientImpersonationContext(HttpContext context)
		{
			this.Start(context, true);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00027A9E File Offset: 0x00025C9E
		internal ClientImpersonationContext(HttpContext context, bool throwOnError)
		{
			this.Start(context, throwOnError);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00027AB0 File Offset: 0x00025CB0
		private void Start(HttpContext context, bool throwOnError)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				if (context != null)
				{
					intPtr = context.ImpersonationToken;
				}
				else
				{
					intPtr = HostingEnvironment.ApplicationIdentityToken;
				}
			}
			catch
			{
				if (throwOnError)
				{
					throw;
				}
			}
			if (intPtr != IntPtr.Zero)
			{
				base.ImpersonateToken(new HandleRef(this, intPtr));
			}
		}
	}
}
