using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000596 RID: 1430
	[Guid("00020401-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeInfo
	{
		// Token: 0x06003426 RID: 13350
		void GetTypeAttr(out IntPtr ppTypeAttr);

		// Token: 0x06003427 RID: 13351
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x06003428 RID: 13352
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		// Token: 0x06003429 RID: 13353
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		// Token: 0x0600342A RID: 13354
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		// Token: 0x0600342B RID: 13355
		void GetRefTypeOfImplType(int index, out int href);

		// Token: 0x0600342C RID: 13356
		void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

		// Token: 0x0600342D RID: 13357
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		// Token: 0x0600342E RID: 13358
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

		// Token: 0x0600342F RID: 13359
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x06003430 RID: 13360
		void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

		// Token: 0x06003431 RID: 13361
		void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

		// Token: 0x06003432 RID: 13362
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		// Token: 0x06003433 RID: 13363
		void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		// Token: 0x06003434 RID: 13364
		void GetMops(int memid, out string pBstrMops);

		// Token: 0x06003435 RID: 13365
		void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

		// Token: 0x06003436 RID: 13366
		[PreserveSig]
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		// Token: 0x06003437 RID: 13367
		[PreserveSig]
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		// Token: 0x06003438 RID: 13368
		[PreserveSig]
		void ReleaseVarDesc(IntPtr pVarDesc);
	}
}
