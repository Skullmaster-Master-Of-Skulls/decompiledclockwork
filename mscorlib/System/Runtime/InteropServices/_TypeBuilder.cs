using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000814 RID: 2068
	[CLSCompliant(false)]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibImportClass(typeof(TypeBuilder))]
	[Guid("7E5678EE-48B3-3F83-B076-C58543498A58")]
	public interface _TypeBuilder
	{
		// Token: 0x0600490E RID: 18702
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600490F RID: 18703
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004910 RID: 18704
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004911 RID: 18705
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
