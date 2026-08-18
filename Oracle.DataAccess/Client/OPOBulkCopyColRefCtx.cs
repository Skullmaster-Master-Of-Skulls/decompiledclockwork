using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000149 RID: 329
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OPOBulkCopyColRefCtx
	{
		// Token: 0x04000A50 RID: 2640
		public string pColName;

		// Token: 0x04000A51 RID: 2641
		public string pObjTypeName;
	}
}
