using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200080B RID: 2059
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CE1A3BF5-975E-30CC-97C9-1EF70F8F3993")]
	[ComVisible(true)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(FieldBuilder))]
	public interface _FieldBuilder
	{
		// Token: 0x060048EA RID: 18666
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060048EB RID: 18667
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060048EC RID: 18668
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060048ED RID: 18669
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
