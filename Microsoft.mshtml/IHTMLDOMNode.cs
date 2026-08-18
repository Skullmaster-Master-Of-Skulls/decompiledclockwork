using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000068 RID: 104
	[TypeLibType(4160)]
	[Guid("3050F5DA-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLDOMNode
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06000AED RID: 2797
		[DispId(-2147417066)]
		int nodeType { [DispId(-2147417066)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06000AEE RID: 2798
		[DispId(-2147417065)]
		IHTMLDOMNode parentNode { [DispId(-2147417065)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x06000AEF RID: 2799
		[DispId(-2147417064)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool hasChildNodes();

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06000AF0 RID: 2800
		[DispId(-2147417063)]
		object childNodes { [DispId(-2147417063)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06000AF1 RID: 2801
		[DispId(-2147417062)]
		object attributes { [DispId(-2147417062)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x06000AF2 RID: 2802
		[DispId(-2147417061)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode insertBefore([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode newChild, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object refChild);

		// Token: 0x06000AF3 RID: 2803
		[DispId(-2147417060)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode removeChild([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000AF4 RID: 2804
		[DispId(-2147417059)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode replaceChild([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000AF5 RID: 2805
		[DispId(-2147417051)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06000AF6 RID: 2806
		[DispId(-2147417046)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06000AF7 RID: 2807
		[DispId(-2147417044)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode swapNode([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06000AF8 RID: 2808
		[DispId(-2147417045)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode replaceNode([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode replacement);

		// Token: 0x06000AF9 RID: 2809
		[DispId(-2147417039)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode appendChild([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode newChild);

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06000AFA RID: 2810
		[DispId(-2147417038)]
		string nodeName { [DispId(-2147417038)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06000AFC RID: 2812
		// (set) Token: 0x06000AFB RID: 2811
		[DispId(-2147417037)]
		object nodeValue { [DispId(-2147417037)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147417037)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06000AFD RID: 2813
		[DispId(-2147417036)]
		IHTMLDOMNode firstChild { [DispId(-2147417036)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06000AFE RID: 2814
		[DispId(-2147417035)]
		IHTMLDOMNode lastChild { [DispId(-2147417035)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06000AFF RID: 2815
		[DispId(-2147417034)]
		IHTMLDOMNode previousSibling { [DispId(-2147417034)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06000B00 RID: 2816
		[DispId(-2147417033)]
		IHTMLDOMNode nextSibling { [DispId(-2147417033)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
