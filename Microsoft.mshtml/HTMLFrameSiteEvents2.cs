using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B44 RID: 2884
	[InterfaceType(2)]
	[Guid("3050F7FF-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface HTMLFrameSiteEvents2
	{
		// Token: 0x06012FF9 RID: 77817
		[DispId(-2147418102)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onhelp([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012FFA RID: 77818
		[DispId(-600)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onclick([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012FFB RID: 77819
		[DispId(-601)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondblclick([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012FFC RID: 77820
		[DispId(-603)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onkeypress([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012FFD RID: 77821
		[DispId(-602)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onkeydown([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012FFE RID: 77822
		[DispId(-604)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onkeyup([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012FFF RID: 77823
		[DispId(-2147418103)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseout([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013000 RID: 77824
		[DispId(-2147418104)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseover([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013001 RID: 77825
		[DispId(-606)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmousemove([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013002 RID: 77826
		[DispId(-605)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmousedown([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013003 RID: 77827
		[DispId(-607)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseup([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013004 RID: 77828
		[DispId(-2147418100)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onselectstart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013005 RID: 77829
		[DispId(-2147418095)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfilterchange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013006 RID: 77830
		[DispId(-2147418101)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondragstart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013007 RID: 77831
		[DispId(-2147418108)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforeupdate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013008 RID: 77832
		[DispId(-2147418107)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onafterupdate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013009 RID: 77833
		[DispId(-2147418099)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onerrorupdate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601300A RID: 77834
		[DispId(-2147418106)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onrowexit([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601300B RID: 77835
		[DispId(-2147418105)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowenter([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601300C RID: 77836
		[DispId(-2147418098)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondatasetchanged([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601300D RID: 77837
		[DispId(-2147418097)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondataavailable([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601300E RID: 77838
		[DispId(-2147418096)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondatasetcomplete([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601300F RID: 77839
		[DispId(-2147418094)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onlosecapture([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013010 RID: 77840
		[DispId(-2147418093)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onpropertychange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013011 RID: 77841
		[DispId(1014)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onscroll([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013012 RID: 77842
		[DispId(-2147418111)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocus([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013013 RID: 77843
		[DispId(-2147418112)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onblur([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013014 RID: 77844
		[DispId(1016)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onresize([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013015 RID: 77845
		[DispId(-2147418092)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondrag([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013016 RID: 77846
		[DispId(-2147418091)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondragend([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013017 RID: 77847
		[DispId(-2147418090)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondragenter([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013018 RID: 77848
		[DispId(-2147418089)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondragover([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013019 RID: 77849
		[DispId(-2147418088)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondragleave([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601301A RID: 77850
		[DispId(-2147418087)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondrop([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601301B RID: 77851
		[DispId(-2147418083)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforecut([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601301C RID: 77852
		[DispId(-2147418086)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncut([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601301D RID: 77853
		[DispId(-2147418082)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforecopy([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601301E RID: 77854
		[DispId(-2147418085)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncopy([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601301F RID: 77855
		[DispId(-2147418081)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforepaste([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013020 RID: 77856
		[DispId(-2147418084)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onpaste([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013021 RID: 77857
		[DispId(1023)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncontextmenu([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013022 RID: 77858
		[DispId(-2147418080)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowsdelete([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013023 RID: 77859
		[DispId(-2147418079)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowsinserted([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013024 RID: 77860
		[DispId(-2147418078)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void oncellchange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013025 RID: 77861
		[DispId(-609)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onreadystatechange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013026 RID: 77862
		[DispId(1030)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onlayoutcomplete([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013027 RID: 77863
		[DispId(1031)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onpage([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013028 RID: 77864
		[DispId(1042)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseenter([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013029 RID: 77865
		[DispId(1043)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseleave([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601302A RID: 77866
		[DispId(1044)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601302B RID: 77867
		[DispId(1045)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondeactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601302C RID: 77868
		[DispId(1034)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforedeactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601302D RID: 77869
		[DispId(1047)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforeactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601302E RID: 77870
		[DispId(1048)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocusin([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601302F RID: 77871
		[DispId(1049)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocusout([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013030 RID: 77872
		[DispId(1035)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmove([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013031 RID: 77873
		[DispId(1036)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncontrolselect([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013032 RID: 77874
		[DispId(1038)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onmovestart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013033 RID: 77875
		[DispId(1039)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmoveend([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013034 RID: 77876
		[DispId(1040)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onresizestart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013035 RID: 77877
		[DispId(1041)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onresizeend([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013036 RID: 77878
		[DispId(1033)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onmousewheel([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06013037 RID: 77879
		[DispId(1003)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onload([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);
	}
}
