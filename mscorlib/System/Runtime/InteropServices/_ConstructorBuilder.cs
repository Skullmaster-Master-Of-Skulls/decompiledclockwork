using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000807 RID: 2055
	[Guid("ED3E4384-D7E2-3FA7-8FFD-8940D330519A")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(ConstructorBuilder))]
	public interface _ConstructorBuilder
	{
		// Token: 0x060048DA RID: 18650
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060048DB RID: 18651
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060048DC RID: 18652
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060048DD RID: 18653
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
