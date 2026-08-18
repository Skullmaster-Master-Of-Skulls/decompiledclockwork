using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200014D RID: 333
	internal struct OPOBulkCopyObjCtx
	{
		// Token: 0x04000A6C RID: 2668
		public int RowsInColArr;

		// Token: 0x04000A6D RID: 2669
		public uint ObjType;

		// Token: 0x04000A6E RID: 2670
		public IntPtr pDPFuncCtx;

		// Token: 0x04000A6F RID: 2671
		public IntPtr pDPFuncCtxColArr;

		// Token: 0x04000A70 RID: 2672
		public unsafe OPOBulkCopyColCtx* pOPOBulkCopyColCtx;

		// Token: 0x04000A71 RID: 2673
		public ushort NoOfCols;

		// Token: 0x04000A72 RID: 2674
		public byte bIsFinalType;
	}
}
