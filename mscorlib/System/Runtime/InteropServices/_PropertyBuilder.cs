using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000812 RID: 2066
	[ComVisible(true)]
	[Guid("15F9A479-9397-3A63-ACBD-F51977FB0F02")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(PropertyBuilder))]
	public interface _PropertyBuilder
	{
		// Token: 0x06004906 RID: 18694
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004907 RID: 18695
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004908 RID: 18696
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004909 RID: 18697
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
