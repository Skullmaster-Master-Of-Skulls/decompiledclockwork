using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020002CE RID: 718
	internal sealed class SqlDebugContext : IDisposable
	{
		// Token: 0x060024E0 RID: 9440 RVA: 0x002993F8 File Offset: 0x002987F8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00299418 File Offset: 0x00298818
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

		// Token: 0x060024E2 RID: 9442 RVA: 0x00299488 File Offset: 0x00298888
		~SqlDebugContext()
		{
			this.Dispose(false);
		}

		// Token: 0x04001780 RID: 6016
		internal uint pid;

		// Token: 0x04001781 RID: 6017
		internal uint tid;

		// Token: 0x04001782 RID: 6018
		internal bool active;

		// Token: 0x04001783 RID: 6019
		internal IntPtr pMemMap = ADP.PtrZero;

		// Token: 0x04001784 RID: 6020
		internal IntPtr hMemMap = ADP.PtrZero;

		// Token: 0x04001785 RID: 6021
		internal uint dbgpid;

		// Token: 0x04001786 RID: 6022
		internal bool fOption;

		// Token: 0x04001787 RID: 6023
		internal string machineName;

		// Token: 0x04001788 RID: 6024
		internal string sdiDllName;

		// Token: 0x04001789 RID: 6025
		internal byte[] data;
	}
}
