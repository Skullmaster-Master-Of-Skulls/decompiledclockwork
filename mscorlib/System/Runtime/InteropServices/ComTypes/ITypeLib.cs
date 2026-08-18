using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200059A RID: 1434
	[Guid("00020402-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeLib
	{
		// Token: 0x06003439 RID: 13369
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x0600343A RID: 13370
		void GetTypeInfo(int index, out ITypeInfo ppTI);

		// Token: 0x0600343B RID: 13371
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x0600343C RID: 13372
		void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

		// Token: 0x0600343D RID: 13373
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x0600343E RID: 13374
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x0600343F RID: 13375
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x06003440 RID: 13376
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x06003441 RID: 13377
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x06003442 RID: 13378
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);
	}
}
