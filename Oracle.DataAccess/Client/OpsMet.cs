using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000DE RID: 222
	[SuppressUnmanagedCodeSecurity]
	internal class OpsMet
	{
		// Token: 0x0600081E RID: 2078
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsMetCopyValCtx")]
		public unsafe static extern int CopyValCtx(OpoMetValCtx* pOpoMetValCtxSrc, ref OpoMetValCtx* pOpoMetValCtxDst);

		// Token: 0x0600081F RID: 2079
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsMetFreeValCtx")]
		public unsafe static extern int FreeValCtx(OpoMetValCtx* pOpoMetValCtx);

		// Token: 0x06000820 RID: 2080
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsMetGetValCtx")]
		public unsafe static extern int GetValCtx(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsSqlCtx, OpoSqlValCtx* pOpoSqlValCtx, ref OpoMetValCtx* pOpoMetValCtx);

		// Token: 0x06000821 RID: 2081
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsMetResetValCtx")]
		public unsafe static extern int ResetMetValCtx(IntPtr opsConCtx, OpoSqlValCtx* pOpoSqlValCtx, OpoMetValCtx* pOpoMetValCtx);

		// Token: 0x06000822 RID: 2082
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsGetPrimaryKey")]
		public unsafe static extern int GetPrimaryKey(IntPtr opsConCtx, IntPtr opsErrCtx, OpoMetValCtx* pOpoMetValCtx, int bSchemaTable, int bAddRowid, int bAddToStmtCache);

		// Token: 0x06000823 RID: 2083
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsGetSchemaMetaData")]
		public unsafe static extern int GetSchemaMetaData(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, OpoMetValCtx* pOpoMetValCtx, int AddRowid, int AddToStmtCache);

		// Token: 0x06000824 RID: 2084
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsMetAddRef")]
		public unsafe static extern int AddRef(OpoMetValCtx* pOpoMetValCtx);

		// Token: 0x06000825 RID: 2085
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsMetRelRef")]
		public unsafe static extern void RelRef(OpoMetValCtx* pOpoMetValCtx);
	}
}
