using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000576 RID: 1398
	[Guid("3050F61B-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(2)]
	[TypeLibType(4112)]
	[ComImport]
	public interface HTMLInputImageEvents2
	{
		// Token: 0x06008B9E RID: 35742
		[DispId(-2147418102)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onhelp([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008B9F RID: 35743
		[DispId(-600)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onclick([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BA0 RID: 35744
		[DispId(-601)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondblclick([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BA1 RID: 35745
		[DispId(-603)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onkeypress([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BA2 RID: 35746
		[DispId(-602)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onkeydown([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BA3 RID: 35747
		[DispId(-604)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onkeyup([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BA4 RID: 35748
		[DispId(-2147418103)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseout([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BA5 RID: 35749
		[DispId(-2147418104)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseover([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BA6 RID: 35750
		[DispId(-606)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmousemove([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BA7 RID: 35751
		[DispId(-605)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmousedown([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BA8 RID: 35752
		[DispId(-607)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseup([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BA9 RID: 35753
		[DispId(-2147418100)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onselectstart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BAA RID: 35754
		[DispId(-2147418095)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfilterchange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BAB RID: 35755
		[DispId(-2147418101)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondragstart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BAC RID: 35756
		[DispId(-2147418108)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforeupdate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BAD RID: 35757
		[DispId(-2147418107)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onafterupdate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BAE RID: 35758
		[DispId(-2147418099)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onerrorupdate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BAF RID: 35759
		[DispId(-2147418106)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onrowexit([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BB0 RID: 35760
		[DispId(-2147418105)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowenter([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BB1 RID: 35761
		[DispId(-2147418098)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondatasetchanged([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BB2 RID: 35762
		[DispId(-2147418097)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondataavailable([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BB3 RID: 35763
		[DispId(-2147418096)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondatasetcomplete([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BB4 RID: 35764
		[DispId(-2147418094)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onlosecapture([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BB5 RID: 35765
		[DispId(-2147418093)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onpropertychange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BB6 RID: 35766
		[DispId(1014)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onscroll([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BB7 RID: 35767
		[DispId(-2147418111)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocus([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BB8 RID: 35768
		[DispId(-2147418112)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onblur([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BB9 RID: 35769
		[DispId(1016)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onresize([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BBA RID: 35770
		[DispId(-2147418092)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondrag([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BBB RID: 35771
		[DispId(-2147418091)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondragend([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BBC RID: 35772
		[DispId(-2147418090)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondragenter([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BBD RID: 35773
		[DispId(-2147418089)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondragover([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BBE RID: 35774
		[DispId(-2147418088)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondragleave([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BBF RID: 35775
		[DispId(-2147418087)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondrop([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BC0 RID: 35776
		[DispId(-2147418083)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforecut([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BC1 RID: 35777
		[DispId(-2147418086)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncut([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BC2 RID: 35778
		[DispId(-2147418082)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforecopy([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BC3 RID: 35779
		[DispId(-2147418085)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncopy([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BC4 RID: 35780
		[DispId(-2147418081)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforepaste([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BC5 RID: 35781
		[DispId(-2147418084)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onpaste([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BC6 RID: 35782
		[DispId(1023)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncontextmenu([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BC7 RID: 35783
		[DispId(-2147418080)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowsdelete([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BC8 RID: 35784
		[DispId(-2147418079)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowsinserted([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BC9 RID: 35785
		[DispId(-2147418078)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void oncellchange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BCA RID: 35786
		[DispId(-609)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onreadystatechange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BCB RID: 35787
		[DispId(1030)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onlayoutcomplete([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BCC RID: 35788
		[DispId(1031)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onpage([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BCD RID: 35789
		[DispId(1042)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseenter([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BCE RID: 35790
		[DispId(1043)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseleave([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BCF RID: 35791
		[DispId(1044)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BD0 RID: 35792
		[DispId(1045)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondeactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BD1 RID: 35793
		[DispId(1034)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforedeactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BD2 RID: 35794
		[DispId(1047)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforeactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BD3 RID: 35795
		[DispId(1048)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocusin([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BD4 RID: 35796
		[DispId(1049)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocusout([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BD5 RID: 35797
		[DispId(1035)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmove([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BD6 RID: 35798
		[DispId(1036)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncontrolselect([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BD7 RID: 35799
		[DispId(1038)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onmovestart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BD8 RID: 35800
		[DispId(1039)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmoveend([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BD9 RID: 35801
		[DispId(1040)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onresizestart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BDA RID: 35802
		[DispId(1041)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onresizeend([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BDB RID: 35803
		[DispId(1033)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onmousewheel([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BDC RID: 35804
		[DispId(1003)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onload([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BDD RID: 35805
		[DispId(1002)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onerror([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008BDE RID: 35806
		[DispId(1000)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onabort([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);
	}
}
