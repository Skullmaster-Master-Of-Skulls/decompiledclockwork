using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000024 RID: 36
	[SuppressUnmanagedCodeSecurity]
	internal class OpsUdt
	{
		// Token: 0x0600016A RID: 362
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtSetSig")]
		public static extern int SetSig(IntPtr pOpsConCtx, out int pSessionBegin);

		// Token: 0x0600016B RID: 363
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtDispose")]
		public static extern int Dispose(IntPtr pOpsConCtx, int SessionBegin, ref IntPtr pUDT, ref IntPtr pOCIRef, ref IntPtr pAttrRefTDO, ref IntPtr pAttrTDO);

		// Token: 0x0600016C RID: 364
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsUdtFromXML")]
		public static extern int UdtFromXML(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDscCtx, ref IntPtr pUDT, ref IntPtr pObjInd, int OCITypeCode, string pXMLStr);

		// Token: 0x0600016D RID: 365
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsUdtToXML")]
		public static extern int UdtToXML(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDscCtx, string schemaNameTypeName, IntPtr pUDT, ref IntPtr LobLocator, ref int pDataLength, int OCITypeCode, int bCheckNotFinal);

		// Token: 0x0600016E RID: 366
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsGetXML")]
		public static extern int GetXML(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDscCtx, IntPtr LobLocator, int DataLength, string pXMLStr);

		// Token: 0x0600016F RID: 367
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtAllocValCtx")]
		public unsafe static extern int AllocValCtx(out OpoUdtValCtx* pOpoUdtValCtx, int numOpoUdtValCtx);

		// Token: 0x06000170 RID: 368
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtReAllocValCtx")]
		public unsafe static extern int ReAllocValCtx(ref OpoUdtValCtx* pOpoUdtValCtx, int numPrevOpoUdtValCtx, int numOpoUdtValCtx);

		// Token: 0x06000171 RID: 369
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtFreeValCtx")]
		public unsafe static extern int FreeValCtx(OpoUdtValCtx* pOpoUdtValCtx, bool bFreeOuter);

		// Token: 0x06000172 RID: 370
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtGetObj")]
		public unsafe static extern int GetObj(IntPtr pOpsConCtx, OpoUdtValCtx* pOpoUdtValCtx);

		// Token: 0x06000173 RID: 371
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtGetArr")]
		public unsafe static extern int GetArr(IntPtr pOpsConCtx, OpoUdtValCtx* pOpoUdtValCtx);

		// Token: 0x06000174 RID: 372
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtGetBFile")]
		public unsafe static extern int GetBFile(IntPtr pOpsConCtx, OpoUdtValCtx* pOpoUdtValCtx, int index);

		// Token: 0x06000175 RID: 373
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtGetLob")]
		public unsafe static extern int GetLob(IntPtr pOpsConCtx, OpoUdtValCtx* pOpoUdtValCtx, int index);

		// Token: 0x06000176 RID: 374
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtGetRef")]
		public unsafe static extern int GetRef(IntPtr pOpsConCtx, OpoUdtValCtx* pOpoUdtValCtx, int index);

		// Token: 0x06000177 RID: 375
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtGetXml")]
		public unsafe static extern int GetXml(IntPtr pOpsConCtx, OpoUdtValCtx* pOpoUdtValCtx, int index);

		// Token: 0x06000178 RID: 376
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtGetUdt")]
		public unsafe static extern int GetUdt(IntPtr pOpsConCtx, OpoUdtValCtx* pOpoUdtValCtx, int index);

		// Token: 0x06000179 RID: 377
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtSetData")]
		public unsafe static extern int SetData(IntPtr pOpsConCtx, OpoUdtValCtx* pOpoUdtValCtx);

		// Token: 0x0600017A RID: 378
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsUdtSetArrayData")]
		public unsafe static extern int SetArrayData(IntPtr pOpsConCtx, OpoUdtValCtx* pOpoUdtValCtx);

		// Token: 0x0600017B RID: 379
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsUdtCopy")]
		public unsafe static extern int Copy(IntPtr opsConCtx, OpoUdtValCtx* pOpoUdtValCtx, IntPtr pObjTarget, IntPtr pObjTargetInd);
	}
}
