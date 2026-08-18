using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x020006BD RID: 1725
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	internal class COAUTHIDENTITY
	{
		// Token: 0x0600534A RID: 21322 RVA: 0x00124B94 File Offset: 0x00122D94
		internal COAUTHIDENTITY(string usr, string dom, string pwd)
		{
			this.user = usr;
			this.userlen = ((this.user == null) ? 0 : this.user.Length);
			this.domain = dom;
			this.domainlen = ((this.domain == null) ? 0 : this.domain.Length);
			this.password = pwd;
			this.passwordlen = ((this.password == null) ? 0 : this.password.Length);
		}

		// Token: 0x04002BC3 RID: 11203
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string user;

		// Token: 0x04002BC4 RID: 11204
		internal int userlen;

		// Token: 0x04002BC5 RID: 11205
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string domain;

		// Token: 0x04002BC6 RID: 11206
		internal int domainlen;

		// Token: 0x04002BC7 RID: 11207
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string password;

		// Token: 0x04002BC8 RID: 11208
		internal int passwordlen;

		// Token: 0x04002BC9 RID: 11209
		internal int flags = 2;
	}
}
