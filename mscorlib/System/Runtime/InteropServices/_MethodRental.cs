using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200080F RID: 2063
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C2323C25-F57F-3880-8A4D-12EBEA7A5852")]
	[ComVisible(true)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(MethodRental))]
	public interface _MethodRental
	{
		// Token: 0x060048FA RID: 18682
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060048FB RID: 18683
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060048FC RID: 18684
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060048FD RID: 18685
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
