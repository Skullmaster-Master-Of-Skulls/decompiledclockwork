using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000D9 RID: 217
	internal struct ColMetaVal
	{
		// Token: 0x040006A4 RID: 1700
		public ushort Ordinal;

		// Token: 0x040006A5 RID: 1701
		public ushort OraType;

		// Token: 0x040006A6 RID: 1702
		public int Size;

		// Token: 0x040006A7 RID: 1703
		public byte Precision;

		// Token: 0x040006A8 RID: 1704
		public sbyte Scale;

		// Token: 0x040006A9 RID: 1705
		public byte NullOK;

		// Token: 0x040006AA RID: 1706
		public byte Updatable;

		// Token: 0x040006AB RID: 1707
		public byte bIsUnique;

		// Token: 0x040006AC RID: 1708
		public byte bIsKeyColumn;

		// Token: 0x040006AD RID: 1709
		public byte bIsHiddenCol;

		// Token: 0x040006AE RID: 1710
		public byte bIsExpression;

		// Token: 0x040006AF RID: 1711
		public int bIsByteSemantic;

		// Token: 0x040006B0 RID: 1712
		public uint Offset;

		// Token: 0x040006B1 RID: 1713
		public DacDef Define;

		// Token: 0x040006B2 RID: 1714
		public ushort CharSetForm;

		// Token: 0x040006B3 RID: 1715
		public ushort UCS2Character;

		// Token: 0x040006B4 RID: 1716
		public ushort ROWIDOrd;

		// Token: 0x040006B5 RID: 1717
		public int bIsXmlType;

		// Token: 0x040006B6 RID: 1718
		public IntPtr pOpsDscCtx;

		// Token: 0x040006B7 RID: 1719
		public int ociTypeCode;

		// Token: 0x040006B8 RID: 1720
		public int bIsFinalType;
	}
}
