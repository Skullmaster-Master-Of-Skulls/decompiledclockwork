using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000568 RID: 1384
	[Guid("3050F7EC-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLSelectionObject2
	{
		// Token: 0x060085FF RID: 34303
		[DispId(1005)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object createRangeCollection();

		// Token: 0x17002CF9 RID: 11513
		// (get) Token: 0x06008600 RID: 34304
		[DispId(1006)]
		string typeDetail { [DispId(1006)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }
	}
}
