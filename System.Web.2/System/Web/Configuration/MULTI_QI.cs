using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x02000719 RID: 1817
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct MULTI_QI : IDisposable
	{
		// Token: 0x0600578B RID: 22411 RVA: 0x00132E45 File Offset: 0x00131045
		internal MULTI_QI(IntPtr pid)
		{
			this.piid = pid;
			this.pItf = IntPtr.Zero;
			this.hr = 0;
		}

		// Token: 0x0600578C RID: 22412 RVA: 0x00132E60 File Offset: 0x00131060
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

		// Token: 0x04002E8C RID: 11916
		internal IntPtr piid;

		// Token: 0x04002E8D RID: 11917
		internal IntPtr pItf;

		// Token: 0x04002E8E RID: 11918
		internal int hr;
	}
}
