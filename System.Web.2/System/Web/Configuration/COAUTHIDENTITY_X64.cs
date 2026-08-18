using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x020006BE RID: 1726
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	internal class COAUTHIDENTITY_X64
	{
		// Token: 0x0600534B RID: 21323 RVA: 0x00124C18 File Offset: 0x00122E18
		internal COAUTHIDENTITY_X64(string usr, string dom, string pwd)
		{
			this.user = usr;
			this.userlen = ((this.user == null) ? 0 : this.user.Length);
			this.domain = dom;
			this.domainlen = ((this.domain == null) ? 0 : this.domain.Length);
			this.password = pwd;
			this.passwordlen = ((this.password == null) ? 0 : this.password.Length);
		}

		// Token: 0x04002BCA RID: 11210
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string user;

		// Token: 0x04002BCB RID: 11211
		internal int userlen;

		// Token: 0x04002BCC RID: 11212
		internal int padding1;

		// Token: 0x04002BCD RID: 11213
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string domain;

		// Token: 0x04002BCE RID: 11214
		internal int domainlen;

		// Token: 0x04002BCF RID: 11215
		internal int padding2;

		// Token: 0x04002BD0 RID: 11216
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string password;

		// Token: 0x04002BD1 RID: 11217
		internal int passwordlen;

		// Token: 0x04002BD2 RID: 11218
		internal int flags = 2;
	}
}
