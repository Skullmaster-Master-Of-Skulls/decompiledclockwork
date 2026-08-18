using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000096 RID: 150
	[SuppressUnmanagedCodeSecurity]
	internal class OpsTrace
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x000488EC File Offset: 0x000478EC
		private OpsTrace()
		{
		}

		// Token: 0x0600076A RID: 1898
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "OpsTrace")]
		public static extern void Trace(uint level, params string[] args);

		// Token: 0x0600076B RID: 1899
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int GetRegTraceInfo(out uint TrcLevel, out int StmtCacheSize, out int FetchSize, out int PSPE, out int PerfCounters);

		// Token: 0x0600076C RID: 1900
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SyncInfo(string strTraceFileName, uint ChkConStatus, uint DynamicEnlist, int FetchSize, int OciEvnts, int PerfCounters, int PromotableTxn, int StmtCacheSize, int StmtCacheWithUdts, int ThreadPoolMaxSize, uint TraceLevel, uint TraceOption, uint UdtCacheSize, int FetchArrayPooling);

		// Token: 0x0600076D RID: 1901
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "OpsTraceCreateMiniDump")]
		public static extern int CreateMiniDump(int threadId, IntPtr pExPtrs);

		// Token: 0x0600076E RID: 1902
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "OpsTraceGetLastErrorCode")]
		public static extern int GetLastErrorCode(out int lastErrorCode);
	}
}
