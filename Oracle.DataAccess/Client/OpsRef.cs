using System;
using System.Runtime.InteropServices;
using System.Security;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200002A RID: 42
	[SuppressUnmanagedCodeSecurity]
	internal class OpsRef
	{
		// Token: 0x060001D1 RID: 465
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsRefIsEqual")]
		public static extern int IsEqual(IntPtr opsConCtx, IntPtr pOCIRefSrc, IntPtr pOCIRefTarget, ref int isEqual);

		// Token: 0x060001D2 RID: 466
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsRefIsLocked")]
		public unsafe static extern int IsLocked(IntPtr opsConCtx, IntPtr opsErrCtx, ref OpoObjValCtx* pOpoObjValCtx, ref IntPtr pUDT, ref IntPtr pOCIRef, ref IntPtr pObjInd, ref int isLocked, ref int isPinned, ref int m_pinLatest);

		// Token: 0x060001D3 RID: 467
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsRefToHex")]
		public static extern int ToHex(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr pOCIRef, ref OpoObjRefCtx pOpoObjRefCtx);

		// Token: 0x060001D4 RID: 468
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsRefFlush")]
		public unsafe static extern int Flush(IntPtr opsConCtx, IntPtr opsErrCtx, ref OpoObjValCtx* pOpoObjValCtx, ref IntPtr pUDT, ref IntPtr pOCIRef, ref IntPtr pObjInd, ref int isPinned, ref int m_pinLatest);

		// Token: 0x060001D5 RID: 469
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsRefMarkDelete")]
		public static extern int MarkDelete(IntPtr opsConCtx, IntPtr opsErrCtx, ref IntPtr pUDT, ref IntPtr pObjInd, ref int isPinned, ref IntPtr pOCIRef);

		// Token: 0x060001D6 RID: 470
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsObjectIsDirty")]
		public static extern int IsDirty(IntPtr opsConCtx, IntPtr opsErrCtx, ref IntPtr pUDT, ref IntPtr pObjInd, ref IntPtr pOCIRef, ref int isPinned, ref bool isDirty);

		// Token: 0x060001D7 RID: 471
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsObjectUnMarkByRef")]
		public static extern int UnMarkObjectByRef(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr pOCIRef);

		// Token: 0x060001D8 RID: 472
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsPinObjCOR")]
		public static extern int PinObjCOR(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr pOCIRef, ref IntPtr pUDT, ref IntPtr pObjInd, ref IntPtr complexObjCtx, int nDepthLevel, ref int m_pinLatest);

		// Token: 0x060001D9 RID: 473
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsUnPinObj")]
		public static extern int UnPinObj(IntPtr opsConCtx, IntPtr opsErrCtx, ref IntPtr pUDT, ref int isPinned);

		// Token: 0x060001DA RID: 474
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsPinObj")]
		public static extern int PinObj(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr pOCIRef, ref IntPtr pUDT, ref IntPtr pObjInd, ref int m_pinLatest);

		// Token: 0x060001DB RID: 475
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsRefLock")]
		public static extern int Lock(IntPtr opsConCtx, IntPtr opsErrCtx, bool bWait, ref IntPtr pUDT, ref IntPtr pObjInd, ref IntPtr pOCIRef, ref int isPinned, ref int isLocked);

		// Token: 0x060001DC RID: 476
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsRefPinAndLock")]
		public static extern int PinAndLock(IntPtr opsConCtx, IntPtr opsErrCtx, bool bWait, ref IntPtr pUDT, ref IntPtr pOCIRef, ref IntPtr pObjInd, ref int isLocked, ref IntPtr complexObjCtx, int nDepthLevel, ref int m_pinLatest);

		// Token: 0x060001DD RID: 477
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsRefRefresh")]
		public unsafe static extern int Refresh(IntPtr opsConCtx, IntPtr opsErrCtx, ref OpoObjValCtx* pOpoObjValCtx, ref IntPtr pUDT, ref IntPtr pOCIRef, ref IntPtr pObjInd, ref int isPinned, ref int m_pinLatest);

		// Token: 0x060001DE RID: 478
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsRefGetTypeName")]
		public unsafe static extern int GetTypeName(IntPtr opsConCtx, IntPtr opsErrCtx, ref OpoObjValCtx* pOpoObjValCtx, ref OpoDscRefCtx opoDscRefCtx, ref IntPtr pUDT, ref IntPtr pOCIRef, ref IntPtr pObjInd, ref int isPinned, ref int m_pinLatest);

		// Token: 0x060001DF RID: 479
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsRefGetTableName")]
		public unsafe static extern int GetTableName(IntPtr opsConCtx, IntPtr opsErrCtx, ref OpoObjValCtx* pOpoObjValCtx, ref OpoObjRefCtx pOpoObjRefCtx, ref IntPtr pUDT, ref IntPtr pOCIRef, ref IntPtr pObjInd, ref int isPinned, ref int m_pinLatest);
	}
}
