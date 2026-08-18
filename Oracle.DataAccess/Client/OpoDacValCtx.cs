using System;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000146 RID: 326
	internal struct OpoDacValCtx
	{
		// Token: 0x04000A2D RID: 2605
		public int MemFileCache;

		// Token: 0x04000A2E RID: 2606
		public int ResultsetIndex;

		// Token: 0x04000A2F RID: 2607
		public long OrigFetchSize;

		// Token: 0x04000A30 RID: 2608
		public long FetchSize;

		// Token: 0x04000A31 RID: 2609
		public int CurrentClientRow;

		// Token: 0x04000A32 RID: 2610
		public int CurrentRowPointedTo;

		// Token: 0x04000A33 RID: 2611
		public int Ordinal;

		// Token: 0x04000A34 RID: 2612
		public int Type;

		// Token: 0x04000A35 RID: 2613
		public long FieldOffset;

		// Token: 0x04000A36 RID: 2614
		public int InitialLongFS;

		// Token: 0x04000A37 RID: 2615
		public int InitialLobFS;

		// Token: 0x04000A38 RID: 2616
		public int BufLen;

		// Token: 0x04000A39 RID: 2617
		public int ForUpdate;

		// Token: 0x04000A3A RID: 2618
		public int Wait;

		// Token: 0x04000A3B RID: 2619
		public int AddRowid;

		// Token: 0x04000A3C RID: 2620
		public int AddToStmtCache;

		// Token: 0x04000A3D RID: 2621
		public int RetDataLen;

		// Token: 0x04000A3E RID: 2622
		public int RecordCount;

		// Token: 0x04000A3F RID: 2623
		public int Indicator;

		// Token: 0x04000A40 RID: 2624
		public int IsUnicode;

		// Token: 0x04000A41 RID: 2625
		public IntPtr pBuffer;

		// Token: 0x04000A42 RID: 2626
		public unsafe void* pValCtx;

		// Token: 0x04000A43 RID: 2627
		public IntPtr pUnmanagedBuf;

		// Token: 0x04000A44 RID: 2628
		public IntPtr pUnmanagedValCtx;

		// Token: 0x04000A45 RID: 2629
		public IntPtr pSnapShot;

		// Token: 0x04000A46 RID: 2630
		public IntPtr pLOBCtx;

		// Token: 0x04000A47 RID: 2631
		public IntPtr pUdtNullStruct;

		// Token: 0x04000A48 RID: 2632
		public IntPtr pTDO;

		// Token: 0x04000A49 RID: 2633
		public unsafe OpoUdtValCtx* pOpoUdtValCtx;

		// Token: 0x04000A4A RID: 2634
		public IntPtr ppRefTDO;
	}
}
