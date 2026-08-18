using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200006F RID: 111
	[Guid("3050F564-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[ComImport]
	public interface DispHTMLDOMAttribute
	{
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06000B24 RID: 2852
		[DispId(1000)]
		string nodeName { [DispId(1000)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06000B26 RID: 2854
		// (set) Token: 0x06000B25 RID: 2853
		[DispId(1002)]
		object nodeValue { [DispId(1002)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(1002)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06000B27 RID: 2855
		[DispId(1001)]
		bool specified { [DispId(1001)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06000B28 RID: 2856
		[DispId(1003)]
		string name { [DispId(1003)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06000B2A RID: 2858
		// (set) Token: 0x06000B29 RID: 2857
		[DispId(1004)]
		string value { [DispId(1004)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1004)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06000B2B RID: 2859
		[DispId(1005)]
		bool expando { [DispId(1005)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06000B2C RID: 2860
		[DispId(1006)]
		int nodeType { [DispId(1006)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06000B2D RID: 2861
		[DispId(1007)]
		IHTMLDOMNode parentNode { [DispId(1007)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06000B2E RID: 2862
		[DispId(1008)]
		object childNodes { [DispId(1008)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06000B2F RID: 2863
		[DispId(1009)]
		IHTMLDOMNode firstChild { [DispId(1009)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06000B30 RID: 2864
		[DispId(1010)]
		IHTMLDOMNode lastChild { [DispId(1010)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06000B31 RID: 2865
		[DispId(1011)]
		IHTMLDOMNode previousSibling { [DispId(1011)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06000B32 RID: 2866
		[DispId(1012)]
		IHTMLDOMNode nextSibling { [DispId(1012)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06000B33 RID: 2867
		[DispId(1013)]
		object attributes { [DispId(1013)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06000B34 RID: 2868
		[DispId(1014)]
		object ownerDocument { [DispId(1014)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x06000B35 RID: 2869
		[DispId(1015)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode insertBefore([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode newChild, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object refChild);

		// Token: 0x06000B36 RID: 2870
		[DispId(1016)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode replaceChild([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000B37 RID: 2871
		[DispId(1017)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode removeChild([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000B38 RID: 2872
		[DispId(1018)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode appendChild([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode newChild);

		// Token: 0x06000B39 RID: 2873
		[DispId(1019)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool hasChildNodes();

		// Token: 0x06000B3A RID: 2874
		[DispId(1020)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMAttribute cloneNode([In] bool fDeep);
	}
}
