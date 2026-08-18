using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200014E RID: 334
	[SuppressUnmanagedCodeSecurity]
	internal class OpsBC
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x00086630 File Offset: 0x00085630
		private OpsBC()
		{
		}

		// Token: 0x06000CF3 RID: 3315
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyAllocBufferNode")]
		public unsafe static extern int AllocBufferNode(ref OPOBufferNode* pBufferNode, int rows, int rowsize);

		// Token: 0x06000CF4 RID: 3316
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyFreeInputBuffer")]
		public unsafe static extern int FreeInputBuffer(OPOBulkCopyValCtx* pOPOBulkCopyValCtx);

		// Token: 0x06000CF5 RID: 3317
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyFreeDataPointers")]
		public unsafe static extern int FreeDataPointers(OPOBulkCopyValCtx* pOPOBulkCopyValCtx);

		// Token: 0x06000CF6 RID: 3318
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyAllocValCtx")]
		public unsafe static extern int AllocValCtx(ref OPOBulkCopyValCtx* pOPOBulkCopyValCtx);

		// Token: 0x06000CF7 RID: 3319
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyFreeValCtx")]
		public unsafe static extern int FreeValCtx(OPOBulkCopyValCtx* pOPOBulkCopyValCtx);

		// Token: 0x06000CF8 RID: 3320
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyAllocColCtx")]
		public unsafe static extern int AllocColCtx(ref OPOBulkCopyColCtx* pOPOBulkCopyColCtx, int colCount);

		// Token: 0x06000CF9 RID: 3321
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyFreeColCtx")]
		public unsafe static extern int FreeColCtx(OPOBulkCopyColCtx* pOPOBulkCopyColCtx, int colCount);

		// Token: 0x06000CFA RID: 3322
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyCopyColCtx")]
		public unsafe static extern int CopyColCtx(OPOBulkCopyColCtx* pSrcColCtx, OPOBulkCopyColCtx* pDstColCtx);

		// Token: 0x06000CFB RID: 3323
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyGetMeta")]
		public unsafe static extern int GetMeta(IntPtr opsConCtx, ref IntPtr opsErrCtx, ref IntPtr opsSqlCtx, string pCommandText, OPOBulkCopyValCtx* pOPOBulkCopyValCtx);

		// Token: 0x06000CFC RID: 3324
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyInit")]
		public unsafe static extern int Init(IntPtr opsConCtx, OPOBulkCopyValCtx* pOPOBulkCopyValCtx, IntPtr pOpsErrCtx);

		// Token: 0x06000CFD RID: 3325
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyLoad")]
		public unsafe static extern int Load(IntPtr opsConCtx, OPOBulkCopyValCtx* pOPOBulkCopyValCtx, IntPtr pOpsErrCtx, ref int pBadRowNum, ref int pBadColNum, int IsOraDataReader, IntPtr pOpsDacCtx, OpoMetValCtx* pOpoMetValCtx, OpoDacValCtx* pOpoDacValCtx);

		// Token: 0x06000CFE RID: 3326
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyFinish")]
		public static extern int Finish(IntPtr opsConCtx, IntPtr pOpsBulkCopyCtx, IntPtr pOpsErrCtx);

		// Token: 0x06000CFF RID: 3327
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyAbort")]
		public static extern int Abort(IntPtr pOpsBulkCopyCtx, IntPtr pOpsErrCtx);

		// Token: 0x06000D00 RID: 3328
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyReset")]
		public unsafe static extern int Reset(IntPtr opsConCtx, OPOBulkCopyValCtx* pOPOBulkCopyValCtx, IntPtr pOpsErrCtx);

		// Token: 0x06000D01 RID: 3329
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsBulkCopyCleanup")]
		public unsafe static extern int Cleanup(OPOBulkCopyValCtx* pOPOBulkCopyValCtx);

		// Token: 0x06000D02 RID: 3330
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "OpsBulkCopyConvertToBinaryDouble")]
		public unsafe static extern int ConvertToBinaryDouble(IntPtr lfpContext, string inputVal, byte* pBinaryDouble);

		// Token: 0x06000D03 RID: 3331
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "OpsBulkCopyConvertToBinaryFloat")]
		public unsafe static extern int ConvertToBinaryFloat(IntPtr lfpContext, string inputVal, byte* pBinaryFloat);
	}
}
