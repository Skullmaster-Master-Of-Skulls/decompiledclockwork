using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D06 RID: 3334
	[TypeLibType(4160)]
	[Guid("3050F5CD-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLAppBehavior3
	{
		// Token: 0x170075FA RID: 30202
		// (get) Token: 0x060163CF RID: 91087
		// (set) Token: 0x060163CE RID: 91086
		[DispId(5019)]
		string navigable { [DispId(5019)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(5019)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }
	}
}
