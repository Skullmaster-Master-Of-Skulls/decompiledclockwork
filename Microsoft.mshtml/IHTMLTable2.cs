using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009F6 RID: 2550
	[Guid("3050F4AD-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLTable2
	{
		// Token: 0x060103C1 RID: 66497
		[DispId(1035)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void firstPage();

		// Token: 0x060103C2 RID: 66498
		[DispId(1036)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void lastPage();

		// Token: 0x17005572 RID: 21874
		// (get) Token: 0x060103C3 RID: 66499
		[DispId(1037)]
		IHTMLElementCollection cells { [DispId(1037)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x060103C4 RID: 66500
		[DispId(1038)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object moveRow([In] int indexFrom = -1, [In] int indexTo = -1);
	}
}
