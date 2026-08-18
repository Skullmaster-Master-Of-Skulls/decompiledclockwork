using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x020006CB RID: 1739
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	internal class COSERVERINFO_X64 : IDisposable
	{
		// Token: 0x060053D8 RID: 21464 RVA: 0x001269C0 File Offset: 0x00124BC0
		internal COSERVERINFO_X64(string srvname, IntPtr authinf)
		{
			this.servername = srvname;
			this.authinfo = authinf;
		}

		// Token: 0x060053D9 RID: 21465 RVA: 0x001269D6 File Offset: 0x00124BD6
		void IDisposable.Dispose()
		{
			this.authinfo = IntPtr.Zero;
			GC.SuppressFinalize(this);
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x001269EC File Offset: 0x00124BEC
		~COSERVERINFO_X64()
		{
		}

		// Token: 0x04002C1C RID: 11292
		internal int reserved1;

		// Token: 0x04002C1D RID: 11293
		internal int padding1;

		// Token: 0x04002C1E RID: 11294
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string servername;

		// Token: 0x04002C1F RID: 11295
		internal IntPtr authinfo;

		// Token: 0x04002C20 RID: 11296
		internal int reserved2;

		// Token: 0x04002C21 RID: 11297
		internal int padding2;
	}
}
