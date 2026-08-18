using System;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000006 RID: 6
	internal struct OpoDscValCtx
	{
		// Token: 0x04000007 RID: 7
		public ushort TypeCode;

		// Token: 0x04000008 RID: 8
		public ushort CollTypeCode;

		// Token: 0x04000009 RID: 9
		public uint NumAttrs;

		// Token: 0x0400000A RID: 10
		public byte bIsFinalType;

		// Token: 0x0400000B RID: 11
		public unsafe AttrMetaVal* pAttrMetaVals;

		// Token: 0x0400000C RID: 12
		public IntPtr pAttrMetaRefs;

		// Token: 0x0400000D RID: 13
		public int bDescribedUdt;

		// Token: 0x0400000E RID: 14
		public int bFetchedNumObjAttrs;

		// Token: 0x0400000F RID: 15
		public int bInvalidTDO;

		// Token: 0x04000010 RID: 16
		public byte bIsInstantiableType;

		// Token: 0x04000011 RID: 17
		public int IndSize;

		// Token: 0x04000012 RID: 18
		public int bIsArrayType;
	}
}
