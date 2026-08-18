using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000147 RID: 327
	[SuppressUnmanagedCodeSecurity]
	internal class OpsDac
	{
		// Token: 0x06000CE3 RID: 3299 RVA: 0x00086618 File Offset: 0x00085618
		private OpsDac()
		{
		}

		// Token: 0x06000CE4 RID: 3300
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDacRead")]
		public unsafe static extern int Read(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsSqlCtx, ref IntPtr opsDacCtx, OpoSqlValCtx* pOpoSqlValCtx, OpoMetValCtx* pOpoMetValCtx, OpoDacValCtx* pOpoDacValCtx);

		// Token: 0x06000CE5 RID: 3301
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDacAllocValCtx")]
		public unsafe static extern int AllocValCtx(ref OpoDacValCtx* pOpoDacValCtx);

		// Token: 0x06000CE6 RID: 3302
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDacFreeCtx")]
		public static extern int FreeCtx(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDacCtx, IntPtr opoMetValCtx, IntPtr opoSqlValCtx, int bFreeOCIHnds);

		// Token: 0x06000CE7 RID: 3303
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDacDispose")]
		public unsafe static extern int Dispose(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsSqlCtx, IntPtr opsDacCtx, OpoMetValCtx* pOpoMetValCtx, OpoDacValCtx* pOpoDacValCtx, OpoSqlValCtx* pOpoSqlValCtx, int bFreeOCIHndls);

		// Token: 0x06000CE8 RID: 3304
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDacNextResult")]
		public unsafe static extern int NextResult(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr[] opsSqlCtx, IntPtr opsDacCtx, OpoSqlValCtx* pOpoSqlValCtx, ref OpoMetValCtx* pOpoMetValCtx, OpoDacValCtx* pOpoDacValCtx);

		// Token: 0x06000CE9 RID: 3305
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDacGetType")]
		public unsafe static extern int GetType(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsDacCtx, OpoMetValCtx* pOpoMetValCtx, OpoDacValCtx* pOpoDacValCtx, int bSkip);

		// Token: 0x06000CEA RID: 3306
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDacGetOraType")]
		public unsafe static extern int GetOraType(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsDacCtx, OpoMetValCtx* pOpoMetValCtx, OpoDacValCtx* pOpoDacValCtx);

		// Token: 0x06000CEB RID: 3307
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDacGetInd")]
		public unsafe static extern int GetInd(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsDacCtx, OpoMetValCtx* pOpoMetValCtx, OpoDacValCtx* pOpoDacValCtx);

		// Token: 0x06000CEC RID: 3308
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDacGetAllInd")]
		public unsafe static extern int GetAllInd(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsDacCtx, OpoMetValCtx* pOpoMetValCtx, OpoDacValCtx* pOpoDacValCtx, IntPtr nullIndicator);

		// Token: 0x06000CED RID: 3309
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDacGetLen")]
		public unsafe static extern int GetLen(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsDacCtx, OpoMetValCtx* pOpoMetValCtx, OpoDacValCtx* pOpoDacValCtx);

		// Token: 0x06000CEE RID: 3310
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDacGetPlsqlOutput")]
		public static extern int GetPlsqlOutput(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, [In] [Out] string[] outputlines, ref int rowsToFetch);

		// Token: 0x06000CEF RID: 3311
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDacGetColumnValues")]
		public unsafe static extern int GetColumnValues(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsDacCtx, OpoMetValCtx* pOpoMetValCtx, OpoDacValCtx* pOpoDacValCtx, OracleDbType[] oracleDbTypes, ref IntPtr columnsDataBuffers, long fetchArraylocation, long rowsize, uint[] columnOffset, uint[] columnIndOffset, uint[] columnLenOffset, uint[] colDatOffset, uint[] colDatSize);
	}
}
