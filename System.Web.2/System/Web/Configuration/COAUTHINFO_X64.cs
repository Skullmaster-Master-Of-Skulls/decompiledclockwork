using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x020006C0 RID: 1728
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	internal class COAUTHINFO_X64 : IDisposable
	{
		// Token: 0x0600534F RID: 21327 RVA: 0x00124D0C File Offset: 0x00122F0C
		internal COAUTHINFO_X64(RpcAuthent authent, RpcAuthor author, string serverprinc, RpcLevel level, RpcImpers impers, IntPtr ciptr)
		{
			this.authnsvc = authent;
			this.authzsvc = author;
			this.serverprincname = serverprinc;
			this.authnlevel = level;
			this.impersonationlevel = impers;
			this.authidentitydata = ciptr;
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x00124D41 File Offset: 0x00122F41
		void IDisposable.Dispose()
		{
			this.authidentitydata = IntPtr.Zero;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x00124D54 File Offset: 0x00122F54
		~COAUTHINFO_X64()
		{
		}

		// Token: 0x04002BDA RID: 11226
		internal RpcAuthent authnsvc;

		// Token: 0x04002BDB RID: 11227
		internal RpcAuthor authzsvc;

		// Token: 0x04002BDC RID: 11228
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string serverprincname;

		// Token: 0x04002BDD RID: 11229
		internal RpcLevel authnlevel;

		// Token: 0x04002BDE RID: 11230
		internal RpcImpers impersonationlevel;

		// Token: 0x04002BDF RID: 11231
		internal IntPtr authidentitydata;

		// Token: 0x04002BE0 RID: 11232
		internal int capabilities;

		// Token: 0x04002BE1 RID: 11233
		internal int padding;
	}
}
