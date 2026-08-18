using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x020007FF RID: 2047
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	[Guid("BEBB2505-8B54-3443-AEAD-142A16DD9CC7")]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(AssemblyBuilder))]
	public interface _AssemblyBuilder
	{
		// Token: 0x06004865 RID: 18533
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004866 RID: 18534
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004867 RID: 18535
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004868 RID: 18536
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
