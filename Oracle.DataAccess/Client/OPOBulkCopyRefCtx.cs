using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000148 RID: 328
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OPOBulkCopyRefCtx
	{
		// Token: 0x04000A4B RID: 2635
		public string pTableName;

		// Token: 0x04000A4C RID: 2636
		public string pPartitionName;

		// Token: 0x04000A4D RID: 2637
		public string pSchemaName;

		// Token: 0x04000A4E RID: 2638
		public string pDateFormatString;

		// Token: 0x04000A4F RID: 2639
		public string pObjType;
	}
}
