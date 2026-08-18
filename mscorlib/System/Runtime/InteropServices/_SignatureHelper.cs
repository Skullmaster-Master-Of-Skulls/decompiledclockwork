using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000813 RID: 2067
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Guid("7D13DD37-5A04-393C-BBCA-A5FEA802893D")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibImportClass(typeof(SignatureHelper))]
	public interface _SignatureHelper
	{
		// Token: 0x0600490A RID: 18698
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600490B RID: 18699
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600490C RID: 18700
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600490D RID: 18701
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
