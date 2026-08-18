using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200012A RID: 298
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class NotiTblRef
	{
		// Token: 0x06000C1C RID: 3100 RVA: 0x00078F90 File Offset: 0x00077F90
		public NotiTblRef()
		{
			this.tableName = null;
		}

		// Token: 0x0400098F RID: 2447
		internal string tableName;
	}
}
