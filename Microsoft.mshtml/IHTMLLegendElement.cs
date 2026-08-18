using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BDA RID: 3034
	[TypeLibType(4160)]
	[Guid("3050F3EA-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLLegendElement
	{
		// Token: 0x17006A29 RID: 27177
		// (get) Token: 0x06014130 RID: 82224
		// (set) Token: 0x0601412F RID: 82223
		[DispId(-2147418039)]
		string align { [DispId(-2147418039)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147418039)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }
	}
}
