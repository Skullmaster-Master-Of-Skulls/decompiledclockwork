using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000070 RID: 112
	[SuppressUnmanagedCodeSecurity]
	internal class OpsCom
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x000390CE File Offset: 0x000380CE
		private OpsCom()
		{
		}

		// Token: 0x0600050C RID: 1292
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsComGetClientInfo")]
		public static extern int GetClientInfo(ref IntPtr pOraGlob);

		// Token: 0x0600050D RID: 1293
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsComGetThreadInfo")]
		public static extern int GetThreadInfo(ref IntPtr pOraGlob);

		// Token: 0x0600050E RID: 1294
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsComSetThreadInfo")]
		public static extern int SetThreadInfo(OraGlobStruct pGlob);

		// Token: 0x0600050F RID: 1295
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "OpsComValidateGlobInfo")]
		public static extern int ValidateGlobInfo(IntPtr pNLSCtx, int paramName, string paramValue);

		// Token: 0x06000510 RID: 1296
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsComRefreshGlobInfo")]
		public static extern int RefreshGlobInfo(IntPtr pNLSCtx, out IntPtr pOraGlob, int type);

		// Token: 0x06000511 RID: 1297
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsComAllocNlsCtx")]
		public static extern int AllocNlsCtx(out IntPtr nlsCtx);

		// Token: 0x06000512 RID: 1298
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsComFreeNlsCtx")]
		public static extern int FreeNlsCtx(IntPtr nlsCtx);

		// Token: 0x06000513 RID: 1299
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsComExtProcFlag")]
		public static extern int GetExtProcFlag();

		// Token: 0x06000514 RID: 1300
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsComExf")]
		public static extern void Exf();

		// Token: 0x06000515 RID: 1301
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsParseTnsnamesFile")]
		public static extern int ParseTnsnamesFile(out string tnsAliases, out string port, out string server, out string service, out string protocol);

		// Token: 0x06000516 RID: 1302
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsGetSystemMemoryInfo")]
		public static extern int GetSystemMemoryInfo(ref long availUsableMem, ref long totalPhysMem);

		// Token: 0x06000517 RID: 1303
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsGetAvailPhysMemory")]
		public static extern int GetAvailPhysMemory(ref long availPhysMem);

		// Token: 0x04000368 RID: 872
		public const string ORAOPS_DLL = "OraOps11w.dll";
	}
}
