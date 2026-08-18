using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000082 RID: 130
	internal struct OpoSqlValCtx
	{
		// Token: 0x040003AE RID: 942
		public int ArraySize;

		// Token: 0x040003AF RID: 943
		public int BindByName;

		// Token: 0x040003B0 RID: 944
		public int RowsAffected;

		// Token: 0x040003B1 RID: 945
		public short CommandType;

		// Token: 0x040003B2 RID: 946
		public int AddRowid;

		// Token: 0x040003B3 RID: 947
		public long FetchSize;

		// Token: 0x040003B4 RID: 948
		public int InitialLongFS;

		// Token: 0x040003B5 RID: 949
		public int InitialLobFS;

		// Token: 0x040003B6 RID: 950
		public uint mode;

		// Token: 0x040003B7 RID: 951
		public IntPtr pSnapShot;

		// Token: 0x040003B8 RID: 952
		public int RetIdxForSP;

		// Token: 0x040003B9 RID: 953
		public int ErrCnt;

		// Token: 0x040003BA RID: 954
		public int AddToStmtCache;

		// Token: 0x040003BB RID: 955
		public int LocalParse;

		// Token: 0x040003BC RID: 956
		public unsafe OpoPrmCtx* pOpoPrmCtx;

		// Token: 0x040003BD RID: 957
		public int StmtPrepared;

		// Token: 0x040003BE RID: 958
		public IntPtr FetchArrayLocation;

		// Token: 0x040003BF RID: 959
		public int bPooledFetchArray;

		// Token: 0x040003C0 RID: 960
		public int bIsFromEF;
	}
}
