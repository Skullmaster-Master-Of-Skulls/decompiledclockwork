using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CBC RID: 3260
	[InterfaceType(1)]
	[Guid("3050F4A0-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IMarkupServices
	{
		// Token: 0x060162B6 RID: 90806
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateMarkupPointer([MarshalAs(UnmanagedType.Interface)] out IMarkupPointer ppPointer);

		// Token: 0x060162B7 RID: 90807
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateMarkupContainer([MarshalAs(UnmanagedType.Interface)] out IMarkupContainer ppMarkupContainer);

		// Token: 0x060162B8 RID: 90808
		[MethodImpl(MethodImplOptions.InternalCall)]
		void createElement([In] _ELEMENT_TAG_ID tagID, [In] ref ushort pchAttributes, [MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElement);

		// Token: 0x060162B9 RID: 90809
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CloneElement([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElemCloneThis, [MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElementTheClone);

		// Token: 0x060162BA RID: 90810
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InsertElement([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElementInsert, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162BB RID: 90811
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveElement([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElementRemove);

		// Token: 0x060162BC RID: 90812
		[MethodImpl(MethodImplOptions.InternalCall)]
		void remove([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162BD RID: 90813
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Copy([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerSourceStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerSourceFinish, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerTarget);

		// Token: 0x060162BE RID: 90814
		[MethodImpl(MethodImplOptions.InternalCall)]
		void move([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerSourceStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerSourceFinish, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerTarget);

		// Token: 0x060162BF RID: 90815
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InsertText([In] ref ushort pchText, [In] int cch, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerTarget);

		// Token: 0x060162C0 RID: 90816
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ParseString([In] ref ushort pchHTML, [In] uint dwFlags, [MarshalAs(UnmanagedType.Interface)] out IMarkupContainer ppContainerResult, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer ppPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer ppPointerFinish);

		// Token: 0x060162C1 RID: 90817
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ParseGlobal([ComAliasName("mshtml.wireHGLOBAL")] [In] ref _userHGLOBAL hglobalHTML, [In] uint dwFlags, [MarshalAs(UnmanagedType.Interface)] out IMarkupContainer ppContainerResult, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162C2 RID: 90818
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsScopedElement([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElement, out int pfScoped);

		// Token: 0x060162C3 RID: 90819
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetElementTagId([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElement, out _ELEMENT_TAG_ID ptagId);

		// Token: 0x060162C4 RID: 90820
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTagIDForName([MarshalAs(UnmanagedType.BStr)] [In] string bstrName, out _ELEMENT_TAG_ID ptagId);

		// Token: 0x060162C5 RID: 90821
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetNameForTagID([In] _ELEMENT_TAG_ID tagID, [MarshalAs(UnmanagedType.BStr)] out string pbstrName);

		// Token: 0x060162C6 RID: 90822
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MovePointersToRange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLTxtRange pIRange, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162C7 RID: 90823
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveRangeToPointers([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFinish, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLTxtRange pIRange);

		// Token: 0x060162C8 RID: 90824
		[MethodImpl(MethodImplOptions.InternalCall)]
		void BeginUndoUnit([In] ref ushort pchTitle);

		// Token: 0x060162C9 RID: 90825
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EndUndoUnit();
	}
}
