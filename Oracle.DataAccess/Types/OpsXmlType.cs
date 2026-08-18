using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000122 RID: 290
	[SuppressUnmanagedCodeSecurity]
	internal class OpsXmlType
	{
		// Token: 0x06000BF4 RID: 3060 RVA: 0x00078F78 File Offset: 0x00077F78
		private OpsXmlType()
		{
		}

		// Token: 0x06000BF5 RID: 3061
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeAllocXmlTypeCtxEmpty")]
		public unsafe static extern int AllocXmlTypeCtxEmpty(IntPtr pOpsConCtx, ref IntPtr ppOpsXmlTypeCtx, ref IntPtr ppOpsErrCtx, ref OpoXmlTypeValCtx* ppOpoXmlTypeValCtx, OpoXmlTypeRefCtx pOpoXmlTypeRefCtx);

		// Token: 0x06000BF6 RID: 3062
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeAllocXmlTypeCtx")]
		public unsafe static extern int AllocXmlTypeCtx(IntPtr pOpsConCtx, ref IntPtr ppOpsXmlTypeCtx, ref IntPtr ppOpsErrCtx, ref OpoXmlTypeValCtx* ppOpoXmlTypeValCtx, IntPtr pBuffer, int flag);

		// Token: 0x06000BF7 RID: 3063
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeAllocCtx")]
		public unsafe static extern int AllocCtx(IntPtr pOpsConCtx, ref IntPtr ppOpsErrCtx, ref OpoXmlTypeValCtx* ppOpoXmlTypeValCtx);

		// Token: 0x06000BF8 RID: 3064
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeCopy")]
		public static extern int Copy(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsXmlTypeCtx, ref IntPtr ppNewOpsXmlTypeCtx);

		// Token: 0x06000BF9 RID: 3065
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeFreeCtx")]
		public unsafe static extern int FreeCtx(ref IntPtr ppOpsConCtx, ref IntPtr ppOpsErrCtx, ref OpoXmlTypeValCtx* ppOpoXmlTypeValCtx, int bFreeXmlHnd);

		// Token: 0x06000BFA RID: 3066
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeExtract")]
		public static extern int Extract(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsXmlTypeCtx, string xpathExpr, string nsMap, ref IntPtr ppOpsXmlTypeCtx_result);

		// Token: 0x06000BFB RID: 3067
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeExists")]
		public static extern int Exists(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsXmlTypeCtx, string xpathExpr, string nsMap, ref int pResult);

		// Token: 0x06000BFC RID: 3068
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeTransform")]
		public static extern int Transform(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsXmlTypeCtx, IntPtr pBuffer, int flag, string paramMap, ref IntPtr ppOpsXmlTypeCtx_result);

		// Token: 0x06000BFD RID: 3069
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeUpdateFromString")]
		public static extern int UpdateFromString(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsXmlTypeCtx, string xpathExpr, string nsMap, string newValue);

		// Token: 0x06000BFE RID: 3070
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeUpdateFromXmlType")]
		public static extern int UpdateFromXmlType(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsXmlTypeCtx, string xpathExpr, string nsMap, IntPtr pNewValue);

		// Token: 0x06000BFF RID: 3071
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeValidate")]
		public static extern int Validate(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsXmlTypeCtx, string schemaUrl, ref int pResult);

		// Token: 0x06000C00 RID: 3072
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeIsSchemaBased")]
		public unsafe static extern int IsSchemaBased(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsXmlTypeCtx, ref OpoXmlTypeValCtx* ppOpoXmlTypeValCtx);

		// Token: 0x06000C01 RID: 3073
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeIsFragment")]
		public unsafe static extern void IsFragment(IntPtr pOpsXmlTypeCtx, ref OpoXmlTypeValCtx* ppOpoXmlTypeValCtx);

		// Token: 0x06000C02 RID: 3074
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeGetSchema")]
		public static extern int GetSchema(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pOpsXmlTypeCtx, ref OpoXmlTypeRefCtx ppOpoXmlTypeRefCtx);

		// Token: 0x06000C03 RID: 3075
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeAddRef")]
		public static extern int AddRef(IntPtr pOpsXmlTypeCtx);

		// Token: 0x06000C04 RID: 3076
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeRelRef")]
		public static extern int RelRef(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, ref IntPtr ppOpsXmlTypeCtx, int bFreeOciXmlType);

		// Token: 0x06000C05 RID: 3077
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeAllocNewCtx")]
		public static extern int AllocNewCtx(IntPtr pOpsConCtx, ref IntPtr ppOpsXmlTypeCtx, IntPtr pOCIXmlType, int allocOciXmlType);

		// Token: 0x06000C06 RID: 3078
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsXmlTypeGetOCIXMLType")]
		public static extern int GetOCIXMLType(IntPtr pOpsXmlTypeCtx, ref IntPtr ppOCIXMLType);

		// Token: 0x04000978 RID: 2424
		public const int TYPE_STRING = 1;

		// Token: 0x04000979 RID: 2425
		public const int TYPE_CLOB = 2;

		// Token: 0x0400097A RID: 2426
		public const int TYPE_STREAM = 3;

		// Token: 0x0400097B RID: 2427
		public const int TYPE_XML = 4;
	}
}
