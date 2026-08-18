using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200012D RID: 301
	[SuppressUnmanagedCodeSecurity]
	internal class OpsChgNTFN
	{
		// Token: 0x06000C1E RID: 3102
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsChgNTFNRegisterNotificationCallback")]
		public static extern int RegisterNotificationCallback(OnChangeCallback s_onChangeOpsCallback);

		// Token: 0x06000C1F RID: 3103
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsChgNTFNGetTableInfos")]
		public static extern int GetTableInfos(IntPtr pOpsEnvCtx, IntPtr pOpsErrCtx, int numTables, OracleNotificationType notiType, IntPtr opsChgNTFNDesc, IntPtr notiTblDescs, out IntPtr tableNames);

		// Token: 0x06000C20 RID: 3104
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsChgNTFNGetRowInfos")]
		public static extern int GetRowInfos(IntPtr pOpsEnvCtx, IntPtr pOpsErrCtx, int numRows, IntPtr opsChgNTFNTableDesc, IntPtr notiRowDescs, out IntPtr rowids);

		// Token: 0x06000C21 RID: 3105
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsChgNTFNGetQueryIds")]
		public static extern int GetQueryIds(IntPtr pOpsEnvCtx, IntPtr pOpsErrCtx, IntPtr opsChgNTFNDesc, int queryNum, ref IntPtr query_desc, ref long query_id, ref int numtables);

		// Token: 0x06000C22 RID: 3106
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsChgNTFNFreeNotiTblRefs")]
		public static extern int FreeNotiTblRefs(ref IntPtr tables, int numTable);

		// Token: 0x06000C23 RID: 3107
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsChgNTFNFreeNotiRowRefs")]
		public static extern int FreeNotiRowRefs(ref IntPtr rowids, int numRow);
	}
}
