using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CAB RID: 3243
	[InterfaceType(1)]
	[Guid("09B5EAB0-F997-11D1-93D4-0060B067B86E")]
	[ComImport]
	public interface IEnumInputContext
	{
		// Token: 0x06016257 RID: 90711
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Clone([MarshalAs(UnmanagedType.Interface)] out IEnumInputContext ppEnum);

		// Token: 0x06016258 RID: 90712
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Next([In] uint ulCount, out uint rgInputContext, out uint pcFetched);

		// Token: 0x06016259 RID: 90713
		[MethodImpl(MethodImplOptions.InternalCall)]
		void reset();

		// Token: 0x0601625A RID: 90714
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Skip([In] uint ulCount);
	}
}
