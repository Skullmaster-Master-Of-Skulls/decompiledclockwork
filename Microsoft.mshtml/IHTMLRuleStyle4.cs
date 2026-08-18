using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200004F RID: 79
	[Guid("3050F817-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLRuleStyle4
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000297 RID: 663
		// (set) Token: 0x06000296 RID: 662
		[DispId(-2147412903)]
		string textOverflow { [TypeLibFunc(20)] [DispId(-2147412903)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [TypeLibFunc(20)] [DispId(-2147412903)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000299 RID: 665
		// (set) Token: 0x06000298 RID: 664
		[DispId(-2147412901)]
		object minHeight { [DispId(-2147412901)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412901)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }
	}
}
