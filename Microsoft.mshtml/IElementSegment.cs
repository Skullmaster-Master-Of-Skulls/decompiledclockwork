using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CB2 RID: 3250
	[InterfaceType(1)]
	[Guid("3050F68F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementSegment : ISegment
	{
		// Token: 0x06016287 RID: 90759
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPointers([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIEnd);

		// Token: 0x06016288 RID: 90760
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetElement([MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppIElement);

		// Token: 0x06016289 RID: 90761
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetPrimary([In] int fPrimary);

		// Token: 0x0601628A RID: 90762
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsPrimary(out int pfPrimary);
	}
}
