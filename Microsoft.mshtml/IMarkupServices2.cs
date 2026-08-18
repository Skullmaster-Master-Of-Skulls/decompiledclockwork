using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC0 RID: 3264
	[InterfaceType(1)]
	[Guid("3050F682-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IMarkupServices2 : IMarkupServices
	{
		// Token: 0x060162CA RID: 90826
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateMarkupPointer([MarshalAs(UnmanagedType.Interface)] out IMarkupPointer ppPointer);

		// Token: 0x060162CB RID: 90827
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateMarkupContainer([MarshalAs(UnmanagedType.Interface)] out IMarkupContainer ppMarkupContainer);

		// Token: 0x060162CC RID: 90828
		[MethodImpl(MethodImplOptions.InternalCall)]
		void createElement([In] _ELEMENT_TAG_ID tagID, [In] ref ushort pchAttributes, [MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElement);

		// Token: 0x060162CD RID: 90829
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CloneElement([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElemCloneThis, [MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElementTheClone);

		// Token: 0x060162CE RID: 90830
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InsertElement([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElementInsert, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162CF RID: 90831
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveElement([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElementRemove);

		// Token: 0x060162D0 RID: 90832
		[MethodImpl(MethodImplOptions.InternalCall)]
		void remove([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162D1 RID: 90833
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Copy([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerSourceStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerSourceFinish, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerTarget);

		// Token: 0x060162D2 RID: 90834
		[MethodImpl(MethodImplOptions.InternalCall)]
		void move([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerSourceStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerSourceFinish, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerTarget);

		// Token: 0x060162D3 RID: 90835
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InsertText([In] ref ushort pchText, [In] int cch, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerTarget);

		// Token: 0x060162D4 RID: 90836
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ParseString([In] ref ushort pchHTML, [In] uint dwFlags, [MarshalAs(UnmanagedType.Interface)] out IMarkupContainer ppContainerResult, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer ppPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer ppPointerFinish);

		// Token: 0x060162D5 RID: 90837
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ParseGlobal([ComAliasName("mshtml.wireHGLOBAL")] [In] ref _userHGLOBAL hglobalHTML, [In] uint dwFlags, [MarshalAs(UnmanagedType.Interface)] out IMarkupContainer ppContainerResult, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162D6 RID: 90838
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsScopedElement([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElement, out int pfScoped);

		// Token: 0x060162D7 RID: 90839
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetElementTagId([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElement, out _ELEMENT_TAG_ID ptagId);

		// Token: 0x060162D8 RID: 90840
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTagIDForName([MarshalAs(UnmanagedType.BStr)] [In] string bstrName, out _ELEMENT_TAG_ID ptagId);

		// Token: 0x060162D9 RID: 90841
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetNameForTagID([In] _ELEMENT_TAG_ID tagID, [MarshalAs(UnmanagedType.BStr)] out string pbstrName);

		// Token: 0x060162DA RID: 90842
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MovePointersToRange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLTxtRange pIRange, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162DB RID: 90843
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveRangeToPointers([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFinish, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLTxtRange pIRange);

		// Token: 0x060162DC RID: 90844
		[MethodImpl(MethodImplOptions.InternalCall)]
		void BeginUndoUnit([In] ref ushort pchTitle);

		// Token: 0x060162DD RID: 90845
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EndUndoUnit();

		// Token: 0x060162DE RID: 90846
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ParseGlobalEx([ComAliasName("mshtml.wireHGLOBAL")] [In] ref _userHGLOBAL hglobalHTML, [In] uint dwFlags, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupContainer pContext, [MarshalAs(UnmanagedType.Interface)] out IMarkupContainer ppContainerResult, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162DF RID: 90847
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ValidateElements([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerFinish, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointerTarget, [MarshalAs(UnmanagedType.Interface)] [In] [Out] IMarkupPointer pPointerStatus, [MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElemFailBottom, [MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElemFailTop);

		// Token: 0x060162E0 RID: 90848
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SaveSegmentsToClipboard([MarshalAs(UnmanagedType.Interface)] [In] ISegmentList pSegmentList, [In] uint dwFlags);
	}
}
