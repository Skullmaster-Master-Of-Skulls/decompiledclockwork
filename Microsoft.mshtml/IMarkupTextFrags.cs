using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC3 RID: 3267
	[InterfaceType(1)]
	[Guid("3050F5FA-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IMarkupTextFrags
	{
		// Token: 0x060162FD RID: 90877
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTextFragCount(out int pcFrags);

		// Token: 0x060162FE RID: 90878
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTextFrag([In] int iFrag, [MarshalAs(UnmanagedType.BStr)] out string pbstrFrag, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFrag);

		// Token: 0x060162FF RID: 90879
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveTextFrag([In] int iFrag);

		// Token: 0x06016300 RID: 90880
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InsertTextFrag([In] int iFrag, [MarshalAs(UnmanagedType.BStr)] [In] string bstrInsert, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerInsert);

		// Token: 0x06016301 RID: 90881
		[MethodImpl(MethodImplOptions.InternalCall)]
		void FindTextFragFromMarkupPointer([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFind, out int piFrag, out int pfFragFound);
	}
}
