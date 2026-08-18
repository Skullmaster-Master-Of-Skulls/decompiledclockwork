using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200012C RID: 300
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class NotiRowRef
	{
		// Token: 0x06000C1D RID: 3101 RVA: 0x00078F9F File Offset: 0x00077F9F
		public NotiRowRef()
		{
			this.rowid = null;
		}

		// Token: 0x04000991 RID: 2449
		internal string rowid;
	}
}
