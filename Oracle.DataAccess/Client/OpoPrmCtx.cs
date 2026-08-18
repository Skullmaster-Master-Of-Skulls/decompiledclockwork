using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000D7 RID: 215
	internal struct OpoPrmCtx
	{
		// Token: 0x0400069C RID: 1692
		public byte CtxType;

		// Token: 0x0400069D RID: 1693
		public int NumValCtxElems;

		// Token: 0x0400069E RID: 1694
		public unsafe OpoPrmValCtx* pOpoPrmValCtx;

		// Token: 0x0400069F RID: 1695
		public IntPtr m_pAttrRefTdo;

		// Token: 0x040006A0 RID: 1696
		public IntPtr pOpsConCtx;

		// Token: 0x040006A1 RID: 1697
		private int SessionBegin;

		// Token: 0x040006A2 RID: 1698
		public IntPtr pMemBlock;

		// Token: 0x040006A3 RID: 1699
		public int bInStmtCache;
	}
}
