using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC2 RID: 3266
	[Guid("3050F675-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IMarkupPointer2 : IMarkupPointer
	{
		// Token: 0x060162E2 RID: 90850
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OwningDoc([MarshalAs(UnmanagedType.Interface)] out IHTMLDocument2 ppDoc);

		// Token: 0x060162E3 RID: 90851
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Gravity(out _POINTER_GRAVITY pGravity);

		// Token: 0x060162E4 RID: 90852
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetGravity([In] _POINTER_GRAVITY Gravity);

		// Token: 0x060162E5 RID: 90853
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Cling(out int pfCling);

		// Token: 0x060162E6 RID: 90854
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetCling([In] int fCLing);

		// Token: 0x060162E7 RID: 90855
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Unposition();

		// Token: 0x060162E8 RID: 90856
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsPositioned(out int pfPositioned);

		// Token: 0x060162E9 RID: 90857
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetContainer([MarshalAs(UnmanagedType.Interface)] out IMarkupContainer ppContainer);

		// Token: 0x060162EA RID: 90858
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveAdjacentToElement([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElement, [In] _ELEMENT_ADJACENCY eAdj);

		// Token: 0x060162EB RID: 90859
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToPointer([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointer);

		// Token: 0x060162EC RID: 90860
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToContainer([MarshalAs(UnmanagedType.Interface)] [In] IMarkupContainer pContainer, [In] int fAtStart);

		// Token: 0x060162ED RID: 90861
		[MethodImpl(MethodImplOptions.InternalCall)]
		void left([In] int fMove, out _MARKUP_CONTEXT_TYPE pContext, [MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElement, [In] [Out] ref int pcch, out ushort pchText);

		// Token: 0x060162EE RID: 90862
		[MethodImpl(MethodImplOptions.InternalCall)]
		void right([In] int fMove, out _MARKUP_CONTEXT_TYPE pContext, [MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElement, [In] [Out] ref int pcch, out ushort pchText);

		// Token: 0x060162EF RID: 90863
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CurrentScope([MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElemCurrent);

		// Token: 0x060162F0 RID: 90864
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsLeftOf([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060162F1 RID: 90865
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsLeftOfOrEqualTo([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060162F2 RID: 90866
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsRightOf([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060162F3 RID: 90867
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsRightOfOrEqualTo([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060162F4 RID: 90868
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsEqualTo([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerThat, out int pfAreEqual);

		// Token: 0x060162F5 RID: 90869
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveUnit([In] _MOVEUNIT_ACTION muAction);

		// Token: 0x060162F6 RID: 90870
		[MethodImpl(MethodImplOptions.InternalCall)]
		void findText([In] ref ushort pchFindText, [In] uint dwFlags, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIEndMatch, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIEndSearch);

		// Token: 0x060162F7 RID: 90871
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsAtWordBreak(out int pfAtBreak);

		// Token: 0x060162F8 RID: 90872
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMarkupPosition(out int plMP);

		// Token: 0x060162F9 RID: 90873
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToMarkupPosition([MarshalAs(UnmanagedType.Interface)] [In] IMarkupContainer pContainer, [In] int lMP);

		// Token: 0x060162FA RID: 90874
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveUnitBounded([In] _MOVEUNIT_ACTION muAction, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIBoundary);

		// Token: 0x060162FB RID: 90875
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsInsideURL([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pRight, out int pfResult);

		// Token: 0x060162FC RID: 90876
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToContent([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pIElement, [In] int fAtStart);
	}
}
