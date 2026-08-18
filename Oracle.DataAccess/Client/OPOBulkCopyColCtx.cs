using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200014A RID: 330
	internal struct OPOBulkCopyColCtx
	{
		// Token: 0x04000A52 RID: 2642
		public uint MaxSize;

		// Token: 0x04000A53 RID: 2643
		public uint MaxCharSize;

		// Token: 0x04000A54 RID: 2644
		public uint ColumnFlag;

		// Token: 0x04000A55 RID: 2645
		public uint IsPtrData;

		// Token: 0x04000A56 RID: 2646
		public unsafe OPOBulkCopyObjCtx* pOPOBulkCopyObjCtx;

		// Token: 0x04000A57 RID: 2647
		public IntPtr pOPOBulkCopyColRefCtx;

		// Token: 0x04000A58 RID: 2648
		public ushort Ordinal;

		// Token: 0x04000A59 RID: 2649
		public ushort OraType;

		// Token: 0x04000A5A RID: 2650
		public ushort OraDbType;

		// Token: 0x04000A5B RID: 2651
		public ushort CharsetID;

		// Token: 0x04000A5C RID: 2652
		public byte CharsetForm;

		// Token: 0x04000A5D RID: 2653
		public byte Precision;

		// Token: 0x04000A5E RID: 2654
		public sbyte Scale;
	}
}
