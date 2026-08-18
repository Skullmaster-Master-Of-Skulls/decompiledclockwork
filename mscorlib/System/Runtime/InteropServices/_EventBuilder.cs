using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200080A RID: 2058
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("AADABA99-895D-3D65-9760-B1F12621FAE8")]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(EventBuilder))]
	[ComVisible(true)]
	public interface _EventBuilder
	{
		// Token: 0x060048E6 RID: 18662
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060048E7 RID: 18663
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060048E8 RID: 18664
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060048E9 RID: 18665
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
