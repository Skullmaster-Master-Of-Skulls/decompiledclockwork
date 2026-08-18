using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000A5 RID: 165
	[InterfaceType(2)]
	[Guid("3050F57F-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTCPropertyBehavior
	{
		// Token: 0x06000D88 RID: 3464
		[DispId(-2147417612)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void fireChange();

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06000D8A RID: 3466
		// (set) Token: 0x06000D89 RID: 3465
		[DispId(-2147412971)]
		object value { [DispId(-2147412971)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412971)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }
	}
}
