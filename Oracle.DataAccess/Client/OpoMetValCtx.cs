using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000DB RID: 219
	internal struct OpoMetValCtx
	{
		// Token: 0x040006C0 RID: 1728
		public short NoOfMetaAlloc;

		// Token: 0x040006C1 RID: 1729
		public short NoOfCols;

		// Token: 0x040006C2 RID: 1730
		public short NoOfHiddenCols;

		// Token: 0x040006C3 RID: 1731
		public byte bHasLongCol;

		// Token: 0x040006C4 RID: 1732
		public byte bHasLongLobBFileCol;

		// Token: 0x040006C5 RID: 1733
		public byte bHasXmlType;

		// Token: 0x040006C6 RID: 1734
		public byte bHasUdtType;

		// Token: 0x040006C7 RID: 1735
		public byte bUdtInfoFetched;

		// Token: 0x040006C8 RID: 1736
		public byte bHasDescCol;

		// Token: 0x040006C9 RID: 1737
		public byte bPkFetched;

		// Token: 0x040006CA RID: 1738
		public byte bPkPresent;

		// Token: 0x040006CB RID: 1739
		public byte bPooled;

		// Token: 0x040006CC RID: 1740
		public int InitialLongFS;

		// Token: 0x040006CD RID: 1741
		public int InitialLobFS;

		// Token: 0x040006CE RID: 1742
		public byte bStmtParsed;

		// Token: 0x040006CF RID: 1743
		public unsafe ColMetaVal* pColMetaVal;

		// Token: 0x040006D0 RID: 1744
		public IntPtr pOpoMetRefCtx;

		// Token: 0x040006D1 RID: 1745
		public int CommandType;

		// Token: 0x040006D2 RID: 1746
		public byte bRowidPresent;

		// Token: 0x040006D3 RID: 1747
		public IntPtr pCommandText;

		// Token: 0x040006D4 RID: 1748
		public IntPtr pNewCommandText;

		// Token: 0x040006D5 RID: 1749
		public ushort NoOfBlobCols;

		// Token: 0x040006D6 RID: 1750
		public ushort NoOfClobCols;

		// Token: 0x040006D7 RID: 1751
		public ushort NoOfNClobCols;

		// Token: 0x040006D8 RID: 1752
		public int RefCount;

		// Token: 0x040006D9 RID: 1753
		public int NoOfDescCols;

		// Token: 0x040006DA RID: 1754
		public byte bChgNtfnRowidPresent;
	}
}
