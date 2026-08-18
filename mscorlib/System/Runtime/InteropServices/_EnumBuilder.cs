using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000809 RID: 2057
	[Guid("C7BD73DE-9F85-3290-88EE-090B8BDFE2DF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(EnumBuilder))]
	[ComVisible(true)]
	public interface _EnumBuilder
	{
		// Token: 0x060048E2 RID: 18658
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060048E3 RID: 18659
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060048E4 RID: 18660
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060048E5 RID: 18661
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
