using System;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000D6 RID: 214
	internal struct OpoPrmValCtx
	{
		// Token: 0x04000680 RID: 1664
		public ushort BindType;

		// Token: 0x04000681 RID: 1665
		public int OraDbType;

		// Token: 0x04000682 RID: 1666
		public int Size;

		// Token: 0x04000683 RID: 1667
		public byte Direction;

		// Token: 0x04000684 RID: 1668
		public ushort UCS2Char;

		// Token: 0x04000685 RID: 1669
		public unsafe ushort* alenp;

		// Token: 0x04000686 RID: 1670
		public unsafe short* pInd;

		// Token: 0x04000687 RID: 1671
		public unsafe short* pSrcInd;

		// Token: 0x04000688 RID: 1672
		public IntPtr pBind;

		// Token: 0x04000689 RID: 1673
		public byte CharSetForm;

		// Token: 0x0400068A RID: 1674
		public unsafe void* pTmpVal;

		// Token: 0x0400068B RID: 1675
		public unsafe void* pBltVal;

		// Token: 0x0400068C RID: 1676
		public byte PrmEnumType;

		// Token: 0x0400068D RID: 1677
		public unsafe int* pIndSize;

		// Token: 0x0400068E RID: 1678
		public IntPtr ppInd;

		// Token: 0x0400068F RID: 1679
		public unsafe int* objalenp;

		// Token: 0x04000690 RID: 1680
		public int maxarr_len;

		// Token: 0x04000691 RID: 1681
		public int curelep;

		// Token: 0x04000692 RID: 1682
		public IntPtr pOpsDscCtx;

		// Token: 0x04000693 RID: 1683
		public unsafe short* pTmpInd;

		// Token: 0x04000694 RID: 1684
		public IntPtr ppTempInd;

		// Token: 0x04000695 RID: 1685
		public byte bIsFinalType;

		// Token: 0x04000696 RID: 1686
		public unsafe void* pTDOSubType;

		// Token: 0x04000697 RID: 1687
		public int NumOpoUdtValCtx;

		// Token: 0x04000698 RID: 1688
		public unsafe OpoUdtValCtx* pOpoUdtValCtx;

		// Token: 0x04000699 RID: 1689
		public IntPtr ppRefTDO;

		// Token: 0x0400069A RID: 1690
		public int NumArrBindElems;

		// Token: 0x0400069B RID: 1691
		public IntPtr pArrBindMemBlock;
	}
}
