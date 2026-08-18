using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200007D RID: 125
	[StructLayout(LayoutKind.Sequential)]
	internal class OpoSubscrCtx : IDisposable
	{
		// Token: 0x060005A8 RID: 1448 RVA: 0x0003E76C File Offset: 0x0003D76C
		public OpoSubscrCtx()
		{
			this.opsSubscrCtx = IntPtr.Zero;
			this.opsErrCtx = IntPtr.Zero;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0003E78C File Offset: 0x0003D78C
		~OpoSubscrCtx()
		{
			this.Dispose(false);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0003E7BC File Offset: 0x0003D7BC
		protected virtual void Dispose(bool disposing)
		{
			try
			{
				OpsSubscr.FreeCtx(OracleDependency.s_opsEnvCtx, out this.opsErrCtx, out this.opsSubscrCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0003E804 File Offset: 0x0003D804
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x040003A4 RID: 932
		internal IntPtr opsSubscrCtx;

		// Token: 0x040003A5 RID: 933
		internal IntPtr opsErrCtx;
	}
}
