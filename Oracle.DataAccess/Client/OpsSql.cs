using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000083 RID: 131
	[SuppressUnmanagedCodeSecurity]
	internal class OpsSql
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x0003E8CF File Offset: 0x0003D8CF
		private OpsSql()
		{
		}

		// Token: 0x060005BB RID: 1467
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSqlAllocValCtx")]
		public unsafe static extern int AllocSqlValCtx(ref OpoSqlValCtx* pOpoSqlValCtx);

		// Token: 0x060005BC RID: 1468
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSqlCopyValCtx")]
		public unsafe static extern int CopySqlValCtx(OpoSqlValCtx* pOpoSqlValCtxSrc, ref OpoSqlValCtx* pOpoSqlValCtxDst);

		// Token: 0x060005BD RID: 1469
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSqlFreeCtx")]
		public static extern int FreeCtx(ref IntPtr opsSqlCtx, IntPtr opsErrCtx, int bStmtCache);

		// Token: 0x060005BE RID: 1470
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSqlFreeValCtx")]
		public unsafe static extern int FreeValCtx(OpoSqlValCtx* pOpoSqlValCtx, int bFreeStmtHnd);

		// Token: 0x060005BF RID: 1471
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSqlFreeRefTDOandOCISnapShot")]
		public unsafe static extern int FreeRefTDOandOCISnapShot(OpoPrmCtx* pOpoPrmCtx, OpoSqlValCtx* pOpoSqlValCtx);

		// Token: 0x060005C0 RID: 1472
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSqlExecuteReader")]
		public unsafe static extern int ExecuteReader(IntPtr opsConCtx, ref IntPtr opsErrCtx, ref IntPtr opsSqlCtx, ref IntPtr opsDacCtx, out IntPtr opsReaderErrCtx, IntPtr opsSubscrCtx, ref int isSubscrRegistered, int bchgNTFNExcludeRowidInfo, int bQueryBasedNTFNRegistration, ref long query_id, ref OpoSqlValCtx* pOpoSqlValCtx, string pCommandText, ref OpoDacValCtx* pOpoDacValCtx, [In] [Out] IntPtr[] pOpoPrmValCtx, string[] ppOpoPrmRefCtx, ref OpoMetValCtx* pOpoMetValCtx, int NoOfParams);

		// Token: 0x060005C1 RID: 1473
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSqlPrepare")]
		public unsafe static extern int Prepare(IntPtr opsConCtx, ref IntPtr opsErrCtx, ref IntPtr opsSqlCtx, ref IntPtr opsDacCtx, ref OpoSqlValCtx* pOpoSqlValCtx, string pCommandText, ref OpoMetValCtx* pOpoMetValCtx);

		// Token: 0x060005C2 RID: 1474
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSqlPrepare2")]
		public unsafe static extern int Prepare2(IntPtr opsConCtx, ref IntPtr opsErrCtx, ref IntPtr opsSqlCtx, ref IntPtr opsDacCtx, ref OpoSqlValCtx* pOpoSqlValCtx, string pCommandText, ref IntPtr pUTF8CommandText, ref OpoMetValCtx* pOpoMetValCtx, int prmCnt);

		// Token: 0x060005C3 RID: 1475
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSqlExecuteNonQuery")]
		public unsafe static extern int ExecuteNonQuery(IntPtr opsConCtx, ref IntPtr opsErrCtx, ref IntPtr opsSqlCtx, ref IntPtr opsDacCtx, IntPtr opsSubscrCtx, ref int isSubscrRegistered, int bchgNTFNExcludeRowidInfo, int bQueryBasedNTFNRegistration, ref long query_id, ref OpoSqlValCtx* pOpoSqlValCtx, string pCommandText, ref IntPtr pUTF8CommandText, [In] [Out] IntPtr[] pOpoPrmValCtx, string[] ppOpoPrmRefCtx, ref OpoMetValCtx* pOpoMetValCtx, int prmCnt, int bFromPool);

		// Token: 0x060005C4 RID: 1476
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSqlBreakExecution")]
		public static extern int BreakExecution(IntPtr opsConCtx, ref IntPtr opsErrCtx);
	}
}
