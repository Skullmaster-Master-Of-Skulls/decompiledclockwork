using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C95 RID: 3221
	[InterfaceType(1)]
	[Guid("3050F49F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IMarkupPointer
	{
		// Token: 0x060161EC RID: 90604
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OwningDoc([MarshalAs(UnmanagedType.Interface)] out IHTMLDocument2 ppDoc);

		// Token: 0x060161ED RID: 90605
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Gravity(out _POINTER_GRAVITY pGravity);

		// Token: 0x060161EE RID: 90606
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetGravity([In] _POINTER_GRAVITY Gravity);

		// Token: 0x060161EF RID: 90607
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Cling(out int pfCling);

		// Token: 0x060161F0 RID: 90608
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetCling([In] int fCLing);

		// Token: 0x060161F1 RID: 90609
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Unposition();

		// Token: 0x060161F2 RID: 90610
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsPositioned(out int pfPositioned);

		// Token: 0x060161F3 RID: 90611
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetContainer([MarshalAs(UnmanagedType.Interface)] out IMarkupContainer ppContainer);

		// Token: 0x060161F4 RID: 90612
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveAdjacentToElement([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElement, [In] _ELEMENT_ADJACENCY eAdj);

		// Token: 0x060161F5 RID: 90613
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToPointer([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointer);

		// Token: 0x060161F6 RID: 90614
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToContainer([MarshalAs(UnmanagedType.Interface)] [In] IMarkupContainer pContainer, [In] int fAtStart);

		// Token: 0x060161F7 RID: 90615
		[MethodImpl(MethodImplOptions.InternalCall)]
		void left([In] int fMove, out _MARKUP_CONTEXT_TYPE pContext, [MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElement, [In] [Out] ref int pcch, out ushort pchText);

		// Token: 0x060161F8 RID: 90616
		[MethodImpl(MethodImplOptions.InternalCall)]
		void right([In] int fMove, out _MARKUP_CONTEXT_TYPE pContext, [MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElement, [In] [Out] ref int pcch, out ushort pchText);

		// Token: 0x060161F9 RID: 90617
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CurrentScope([MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElemCurrent);

		// Token: 0x060161FA RID: 90618
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsLeftOf([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060161FB RID: 90619
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsLeftOfOrEqualTo([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060161FC RID: 90620
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsRightOf([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060161FD RID: 90621
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsRightOfOrEqualTo([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060161FE RID: 90622
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsEqualTo([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerThat, out int pfAreEqual);

		// Token: 0x060161FF RID: 90623
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveUnit([In] _MOVEUNIT_ACTION muAction);

		// Token: 0x06016200 RID: 90624
		[MethodImpl(MethodImplOptions.InternalCall)]
		void findText([In] ref ushort pchFindText, [In] uint dwFlags, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIEndMatch, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIEndSearch);
	}
}
