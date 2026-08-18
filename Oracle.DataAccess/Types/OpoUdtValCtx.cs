using System;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000022 RID: 34
	internal struct OpoUdtValCtx
	{
		// Token: 0x040000D9 RID: 217
		public IntPtr pUDT;

		// Token: 0x040000DA RID: 218
		public IntPtr pNullStruct;

		// Token: 0x040000DB RID: 219
		public IntPtr pOpsErrCtx;

		// Token: 0x040000DC RID: 220
		public unsafe OpoDscValCtx* pOpoDscValCtx;

		// Token: 0x040000DD RID: 221
		public IntPtr pTDO;

		// Token: 0x040000DE RID: 222
		public IntPtr ppRefTDO;

		// Token: 0x040000DF RID: 223
		public int bIgnoreElemStatus;

		// Token: 0x040000E0 RID: 224
		public int bIsOdtConnection;

		// Token: 0x040000E1 RID: 225
		public int NumOfArrElems;

		// Token: 0x040000E2 RID: 226
		public unsafe int* pDataLen;

		// Token: 0x040000E3 RID: 227
		public long ArrDataTmpBufferSize;

		// Token: 0x040000E4 RID: 228
		public IntPtr pArrDataTmpBuffer;

		// Token: 0x040000E5 RID: 229
		public long ArrStatusTmpBufferSize;

		// Token: 0x040000E6 RID: 230
		public IntPtr pArrStatusTmpBuffer;

		// Token: 0x040000E7 RID: 231
		public long ArrExistsTmpBufferSize;

		// Token: 0x040000E8 RID: 232
		public IntPtr pArrExistsTmpBuffer;

		// Token: 0x040000E9 RID: 233
		public int NumOpoUdtValCtx;

		// Token: 0x040000EA RID: 234
		public unsafe OpoUdtValCtx* pOpoUdtValCtx;

		// Token: 0x040000EB RID: 235
		public OpoUdtAttrValCtx opoUdtAttrValCtx;

		// Token: 0x040000EC RID: 236
		public int DataLen;

		// Token: 0x040000ED RID: 237
		public long DataBufferSize;

		// Token: 0x040000EE RID: 238
		public IntPtr pDataMarshalBuffer;

		// Token: 0x040000EF RID: 239
		public IntPtr pDataPinnedBuffer;

		// Token: 0x040000F0 RID: 240
		public IntPtr pDataTmpBuffer;

		// Token: 0x040000F1 RID: 241
		public int bIsNull;

		// Token: 0x040000F2 RID: 242
		public long StatusBufferSize;

		// Token: 0x040000F3 RID: 243
		public IntPtr pStatusMarshalBuffer;

		// Token: 0x040000F4 RID: 244
		public IntPtr pStatusPinnedBuffer;
	}
}
