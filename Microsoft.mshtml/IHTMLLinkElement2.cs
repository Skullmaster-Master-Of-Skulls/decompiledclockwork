using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000151 RID: 337
	[Guid("3050F4E5-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLLinkElement2
	{
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060014C7 RID: 5319
		// (set) Token: 0x060014C6 RID: 5318
		[DispId(1017)]
		string target { [TypeLibFunc(20)] [DispId(1017)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [TypeLibFunc(20)] [DispId(1017)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }
	}
}
