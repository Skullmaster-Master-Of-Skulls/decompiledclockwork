using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000D8 RID: 216
	[SuppressUnmanagedCodeSecurity]
	internal class OpsPrm
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x000503A3 File Offset: 0x0004F3A3
		private OpsPrm()
		{
		}

		// Token: 0x06000813 RID: 2067
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsPrmResetValCtx")]
		public unsafe static extern int ResetValCtx(OpoPrmValCtx* pOpoPrmValCtx);

		// Token: 0x06000814 RID: 2068
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsPrmReAllocValCtx")]
		public unsafe static extern int ReAllocValCtx(OpoPrmValCtx* pOpoPrmValCtx, int arraySize);

		// Token: 0x06000815 RID: 2069
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsPrmFreeOpoPrmCtx")]
		public unsafe static extern int FreeOpoPrmCtx(OpoPrmCtx* pOpoPrmCtx);

		// Token: 0x06000816 RID: 2070
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsPrmFreeUdtObjects")]
		public unsafe static extern int FreeUdtObjects(IntPtr pOpsConCtx, OpoPrmValCtx* pOpoPrmValCtx);

		// Token: 0x06000817 RID: 2071
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsPrmFreeUdtInObjects")]
		public unsafe static extern int FreeUdtInObjects(IntPtr pOpsConCtx, OpoPrmValCtx* pOpoPrmValCtx);
	}
}
