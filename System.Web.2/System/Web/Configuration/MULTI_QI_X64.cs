using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x0200071A RID: 1818
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct MULTI_QI_X64 : IDisposable
	{
		// Token: 0x0600578D RID: 22413 RVA: 0x00132ECE File Offset: 0x001310CE
		internal MULTI_QI_X64(IntPtr pid)
		{
			this.piid = pid;
			this.pItf = IntPtr.Zero;
			this.hr = 0;
			this.padding = 0;
		}

		// Token: 0x0600578E RID: 22414 RVA: 0x00132EF0 File Offset: 0x001310F0
		void IDisposable.Dispose()
		{
			if (this.pItf != IntPtr.Zero)
			{
				Marshal.Release(this.pItf);
				this.pItf = IntPtr.Zero;
			}
			if (this.piid != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.piid);
				this.piid = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x04002E8F RID: 11919
		internal IntPtr piid;

		// Token: 0x04002E90 RID: 11920
		internal IntPtr pItf;

		// Token: 0x04002E91 RID: 11921
		internal int hr;

		// Token: 0x04002E92 RID: 11922
		internal int padding;
	}
}
