using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000811 RID: 2065
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Guid("36329EBA-F97A-3565-BC07-0ED5C6EF19FC")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibImportClass(typeof(ParameterBuilder))]
	public interface _ParameterBuilder
	{
		// Token: 0x06004902 RID: 18690
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004903 RID: 18691
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004904 RID: 18692
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004905 RID: 18693
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
