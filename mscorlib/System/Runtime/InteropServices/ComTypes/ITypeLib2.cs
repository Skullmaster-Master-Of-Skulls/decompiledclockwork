using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200059B RID: 1435
	[Guid("00020411-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeLib2 : ITypeLib
	{
		// Token: 0x06003443 RID: 13379
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x06003444 RID: 13380
		void GetTypeInfo(int index, out ITypeInfo ppTI);

		// Token: 0x06003445 RID: 13381
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x06003446 RID: 13382
		void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

		// Token: 0x06003447 RID: 13383
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x06003448 RID: 13384
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x06003449 RID: 13385
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x0600344A RID: 13386
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x0600344B RID: 13387
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x0600344C RID: 13388
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);

		// Token: 0x0600344D RID: 13389
		void GetCustData(ref Guid guid, out object pVarVal);

		// Token: 0x0600344E RID: 13390
		[LCIDConversion(1)]
		void GetDocumentation2(int index, out string pbstrHelpString, out int pdwHelpStringContext, out string pbstrHelpStringDll);

		// Token: 0x0600344F RID: 13391
		void GetLibStatistics(IntPtr pcUniqueNames, out int pcchUniqueNames);

		// Token: 0x06003450 RID: 13392
		void GetAllCustData(IntPtr pCustData);
	}
}
