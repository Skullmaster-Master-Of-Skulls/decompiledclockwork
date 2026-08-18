using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200080E RID: 2062
	[ComVisible(true)]
	[Guid("007D8A14-FDF3-363E-9A0B-FEC0618260A2")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(MethodBuilder))]
	public interface _MethodBuilder
	{
		// Token: 0x060048F6 RID: 18678
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060048F7 RID: 18679
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060048F8 RID: 18680
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060048F9 RID: 18681
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
