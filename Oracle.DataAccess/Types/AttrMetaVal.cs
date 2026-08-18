using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000008 RID: 8
	internal struct AttrMetaVal
	{
		// Token: 0x04000015 RID: 21
		public ushort TypeCode;

		// Token: 0x04000016 RID: 22
		public ushort OraType;

		// Token: 0x04000017 RID: 23
		public byte Precision;

		// Token: 0x04000018 RID: 24
		public sbyte Scale;

		// Token: 0x04000019 RID: 25
		public uint Size;

		// Token: 0x0400001A RID: 26
		public byte CharsetForm;

		// Token: 0x0400001B RID: 27
		public int bDescribed;

		// Token: 0x0400001C RID: 28
		public int Offset;

		// Token: 0x0400001D RID: 29
		public int IndOffset;

		// Token: 0x0400001E RID: 30
		public unsafe OpoDscValCtx* pOpoDscValCtx;

		// Token: 0x0400001F RID: 31
		public int IsNullable;

		// Token: 0x04000020 RID: 32
		public CustomTypeCode CustTypeCode;
	}
}
