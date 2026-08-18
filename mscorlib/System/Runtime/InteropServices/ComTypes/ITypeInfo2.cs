using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200059C RID: 1436
	[Guid("00020412-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeInfo2 : ITypeInfo
	{
		// Token: 0x06003451 RID: 13393
		void GetTypeAttr(out IntPtr ppTypeAttr);

		// Token: 0x06003452 RID: 13394
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x06003453 RID: 13395
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		// Token: 0x06003454 RID: 13396
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		// Token: 0x06003455 RID: 13397
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		// Token: 0x06003456 RID: 13398
		void GetRefTypeOfImplType(int index, out int href);

		// Token: 0x06003457 RID: 13399
		void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

		// Token: 0x06003458 RID: 13400
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		// Token: 0x06003459 RID: 13401
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

		// Token: 0x0600345A RID: 13402
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x0600345B RID: 13403
		void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

		// Token: 0x0600345C RID: 13404
		void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

		// Token: 0x0600345D RID: 13405
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		// Token: 0x0600345E RID: 13406
		void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		// Token: 0x0600345F RID: 13407
		void GetMops(int memid, out string pBstrMops);

		// Token: 0x06003460 RID: 13408
		void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

		// Token: 0x06003461 RID: 13409
		[PreserveSig]
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		// Token: 0x06003462 RID: 13410
		[PreserveSig]
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		// Token: 0x06003463 RID: 13411
		[PreserveSig]
		void ReleaseVarDesc(IntPtr pVarDesc);

		// Token: 0x06003464 RID: 13412
		void GetTypeKind(out TYPEKIND pTypeKind);

		// Token: 0x06003465 RID: 13413
		void GetTypeFlags(out int pTypeFlags);

		// Token: 0x06003466 RID: 13414
		void GetFuncIndexOfMemId(int memid, INVOKEKIND invKind, out int pFuncIndex);

		// Token: 0x06003467 RID: 13415
		void GetVarIndexOfMemId(int memid, out int pVarIndex);

		// Token: 0x06003468 RID: 13416
		void GetCustData(ref Guid guid, out object pVarVal);

		// Token: 0x06003469 RID: 13417
		void GetFuncCustData(int index, ref Guid guid, out object pVarVal);

		// Token: 0x0600346A RID: 13418
		void GetParamCustData(int indexFunc, int indexParam, ref Guid guid, out object pVarVal);

		// Token: 0x0600346B RID: 13419
		void GetVarCustData(int index, ref Guid guid, out object pVarVal);

		// Token: 0x0600346C RID: 13420
		void GetImplTypeCustData(int index, ref Guid guid, out object pVarVal);

		// Token: 0x0600346D RID: 13421
		[LCIDConversion(1)]
		void GetDocumentation2(int memid, out string pbstrHelpString, out int pdwHelpStringContext, out string pbstrHelpStringDll);

		// Token: 0x0600346E RID: 13422
		void GetAllCustData(IntPtr pCustData);

		// Token: 0x0600346F RID: 13423
		void GetAllFuncCustData(int index, IntPtr pCustData);

		// Token: 0x06003470 RID: 13424
		void GetAllParamCustData(int indexFunc, int indexParam, IntPtr pCustData);

		// Token: 0x06003471 RID: 13425
		void GetAllVarCustData(int index, IntPtr pCustData);

		// Token: 0x06003472 RID: 13426
		void GetAllImplTypeCustData(int index, IntPtr pCustData);
	}
}
