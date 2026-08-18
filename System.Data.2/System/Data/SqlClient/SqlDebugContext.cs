using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001B8 RID: 440
	internal sealed class SqlDebugContext : IDisposable
	{
		// Token: 0x06001AC5 RID: 6853 RVA: 0x000BD5CC File Offset: 0x000BC9CC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000BD5E8 File Offset: 0x000BC9E8
		private void Dispose(bool disposing)
		{
			if (this.pMemMap != IntPtr.Zero)
			{
				NativeMethods.UnmapViewOfFile(this.pMemMap);
				this.pMemMap = IntPtr.Zero;
			}
			if (this.hMemMap != IntPtr.Zero)
			{
				NativeMethods.CloseHandle(this.hMemMap);
				this.hMemMap = IntPtr.Zero;
			}
			this.active = false;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x000BD650 File Offset: 0x000BCA50
		~SqlDebugContext()
		{
			this.Dispose(false);
		}

		// Token: 0x04000F81 RID: 3969
		internal uint pid;

		// Token: 0x04000F82 RID: 3970
		internal uint tid;

		// Token: 0x04000F83 RID: 3971
		internal bool active;

		// Token: 0x04000F84 RID: 3972
		internal IntPtr pMemMap = ADP.PtrZero;

		// Token: 0x04000F85 RID: 3973
		internal IntPtr hMemMap = ADP.PtrZero;

		// Token: 0x04000F86 RID: 3974
		internal uint dbgpid;

		// Token: 0x04000F87 RID: 3975
		internal bool fOption;

		// Token: 0x04000F88 RID: 3976
		internal string machineName;

		// Token: 0x04000F89 RID: 3977
		internal string sdiDllName;

		// Token: 0x04000F8A RID: 3978
		internal byte[] data;
	}
}
