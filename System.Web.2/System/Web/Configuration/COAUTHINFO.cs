using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x020006BF RID: 1727
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	internal class COAUTHINFO : IDisposable
	{
		// Token: 0x0600534C RID: 21324 RVA: 0x00124C9B File Offset: 0x00122E9B
		internal COAUTHINFO(RpcAuthent authent, RpcAuthor author, string serverprinc, RpcLevel level, RpcImpers impers, IntPtr ciptr)
		{
			this.authnsvc = authent;
			this.authzsvc = author;
			this.serverprincname = serverprinc;
			this.authnlevel = level;
			this.impersonationlevel = impers;
			this.authidentitydata = ciptr;
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x00124CD0 File Offset: 0x00122ED0
		void IDisposable.Dispose()
		{
			this.authidentitydata = IntPtr.Zero;
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x00124CE4 File Offset: 0x00122EE4
		~COAUTHINFO()
		{
		}

		// Token: 0x04002BD3 RID: 11219
		internal RpcAuthent authnsvc;

		// Token: 0x04002BD4 RID: 11220
		internal RpcAuthor authzsvc;

		// Token: 0x04002BD5 RID: 11221
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string serverprincname;

		// Token: 0x04002BD6 RID: 11222
		internal RpcLevel authnlevel;

		// Token: 0x04002BD7 RID: 11223
		internal RpcImpers impersonationlevel;

		// Token: 0x04002BD8 RID: 11224
		internal IntPtr authidentitydata;

		// Token: 0x04002BD9 RID: 11225
		internal int capabilities;
	}
}
