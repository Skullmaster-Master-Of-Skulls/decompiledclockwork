using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C9E RID: 3230
	[InterfaceType(1)]
	[Guid("4955DD31-B159-11D0-8FCF-00AA006BCC59")]
	[ComImport]
	public interface IEnumRegisterWordW
	{
		// Token: 0x06016253 RID: 90707
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Clone([MarshalAs(UnmanagedType.Interface)] out IEnumRegisterWordW ppEnum);

		// Token: 0x06016254 RID: 90708
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Next([In] uint ulCount, out __MIDL___MIDL_itf_mshtml_0250_0002 rgRegisterWord, out uint pcFetched);

		// Token: 0x06016255 RID: 90709
		[MethodImpl(MethodImplOptions.InternalCall)]
		void reset();

		// Token: 0x06016256 RID: 90710
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Skip([In] uint ulCount);
	}
}
