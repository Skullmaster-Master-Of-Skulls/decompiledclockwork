using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CB7 RID: 3255
	[TypeLibType(4096)]
	[Guid("3050F81A-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHtmlDlgSafeHelper
	{
		// Token: 0x060162A9 RID: 90793
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object choosecolordlg([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object initColor);

		// Token: 0x060162AA RID: 90794
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object getCharset([MarshalAs(UnmanagedType.BStr)] [In] string fontName);

		// Token: 0x170075B1 RID: 30129
		// (get) Token: 0x060162AB RID: 90795
		[DispId(3)]
		object fonts { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x170075B2 RID: 30130
		// (get) Token: 0x060162AC RID: 90796
		[DispId(4)]
		object BlockFormats { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }
	}
}
