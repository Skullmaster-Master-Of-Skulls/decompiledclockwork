using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200014C RID: 332
	internal struct OPOBulkCopyValCtx
	{
		// Token: 0x04000A61 RID: 2657
		public int NoOfRows;

		// Token: 0x04000A62 RID: 2658
		public int RowsInColArr;

		// Token: 0x04000A63 RID: 2659
		public int MaxRowsInBuffer;

		// Token: 0x04000A64 RID: 2660
		public unsafe OPOBulkCopyColCtx* pOPOBulkCopyColCtx;

		// Token: 0x04000A65 RID: 2661
		public IntPtr pOPOBulkCopyRefCtx;

		// Token: 0x04000A66 RID: 2662
		public IntPtr pOPOBulkCopyCtx;

		// Token: 0x04000A67 RID: 2663
		public unsafe OPOBufferNode* pInputBuffer;

		// Token: 0x04000A68 RID: 2664
		public IntPtr OffToRowNum;

		// Token: 0x04000A69 RID: 2665
		public ushort NoOfCols;

		// Token: 0x04000A6A RID: 2666
		public byte NoLog;

		// Token: 0x04000A6B RID: 2667
		public IntPtr lfpContext;
	}
}
