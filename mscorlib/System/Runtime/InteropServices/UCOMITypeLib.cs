using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000566 RID: 1382
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeLib instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00020402-0000-0000-C000-000000000046")]
	[ComImport]
	public interface UCOMITypeLib
	{
		// Token: 0x060033B1 RID: 13233
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x060033B2 RID: 13234
		void GetTypeInfo(int index, out UCOMITypeInfo ppTI);

		// Token: 0x060033B3 RID: 13235
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x060033B4 RID: 13236
		void GetTypeInfoOfGuid(ref Guid guid, out UCOMITypeInfo ppTInfo);

		// Token: 0x060033B5 RID: 13237
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x060033B6 RID: 13238
		void GetTypeComp(out UCOMITypeComp ppTComp);

		// Token: 0x060033B7 RID: 13239
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x060033B8 RID: 13240
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x060033B9 RID: 13241
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] UCOMITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x060033BA RID: 13242
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);
	}
}
