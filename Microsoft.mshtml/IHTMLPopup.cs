using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CF8 RID: 3320
	[Guid("3050F666-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLPopup
	{
		// Token: 0x0601639A RID: 91034
		[DispId(27001)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Show([In] int x, [In] int y, [In] int w, [In] int h, [MarshalAs(UnmanagedType.Struct)] [In] ref object pElement);

		// Token: 0x0601639B RID: 91035
		[DispId(27002)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Hide();

		// Token: 0x170075E0 RID: 30176
		// (get) Token: 0x0601639C RID: 91036
		[DispId(27003)]
		IHTMLDocument document { [DispId(27003)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x170075E1 RID: 30177
		// (get) Token: 0x0601639D RID: 91037
		[DispId(27004)]
		bool isOpen { [DispId(27004)] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
