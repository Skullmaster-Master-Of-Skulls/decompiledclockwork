using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x020006CA RID: 1738
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	internal class COSERVERINFO : IDisposable
	{
		// Token: 0x060053D5 RID: 21461 RVA: 0x0012696C File Offset: 0x00124B6C
		internal COSERVERINFO(string srvname, IntPtr authinf)
		{
			this.servername = srvname;
			this.authinfo = authinf;
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x00126982 File Offset: 0x00124B82
		void IDisposable.Dispose()
		{
			this.authinfo = IntPtr.Zero;
			GC.SuppressFinalize(this);
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x00126998 File Offset: 0x00124B98
		~COSERVERINFO()
		{
		}

		// Token: 0x04002C18 RID: 11288
		internal int reserved1;

		// Token: 0x04002C19 RID: 11289
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string servername;

		// Token: 0x04002C1A RID: 11290
		internal IntPtr authinfo;

		// Token: 0x04002C1B RID: 11291
		internal int reserved2;
	}
}
