using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000955 RID: 2389
	[InterfaceType(2)]
	[Guid("3050F611-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface HTMLAreaEvents2
	{
		// Token: 0x0600EC3C RID: 60476
		[DispId(-2147418102)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onhelp([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC3D RID: 60477
		[DispId(-600)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onclick([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC3E RID: 60478
		[DispId(-601)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondblclick([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC3F RID: 60479
		[DispId(-603)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onkeypress([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC40 RID: 60480
		[DispId(-602)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onkeydown([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC41 RID: 60481
		[DispId(-604)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onkeyup([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC42 RID: 60482
		[DispId(-2147418103)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseout([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC43 RID: 60483
		[DispId(-2147418104)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseover([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC44 RID: 60484
		[DispId(-606)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmousemove([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC45 RID: 60485
		[DispId(-605)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmousedown([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC46 RID: 60486
		[DispId(-607)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseup([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC47 RID: 60487
		[DispId(-2147418100)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onselectstart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC48 RID: 60488
		[DispId(-2147418095)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfilterchange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC49 RID: 60489
		[DispId(-2147418101)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondragstart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC4A RID: 60490
		[DispId(-2147418108)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforeupdate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC4B RID: 60491
		[DispId(-2147418107)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onafterupdate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC4C RID: 60492
		[DispId(-2147418099)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onerrorupdate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC4D RID: 60493
		[DispId(-2147418106)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onrowexit([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC4E RID: 60494
		[DispId(-2147418105)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowenter([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC4F RID: 60495
		[DispId(-2147418098)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondatasetchanged([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC50 RID: 60496
		[DispId(-2147418097)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondataavailable([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC51 RID: 60497
		[DispId(-2147418096)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondatasetcomplete([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC52 RID: 60498
		[DispId(-2147418094)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onlosecapture([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC53 RID: 60499
		[DispId(-2147418093)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onpropertychange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC54 RID: 60500
		[DispId(1014)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onscroll([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC55 RID: 60501
		[DispId(-2147418111)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocus([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC56 RID: 60502
		[DispId(-2147418112)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onblur([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC57 RID: 60503
		[DispId(1016)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onresize([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC58 RID: 60504
		[DispId(-2147418092)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondrag([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC59 RID: 60505
		[DispId(-2147418091)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondragend([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC5A RID: 60506
		[DispId(-2147418090)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondragenter([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC5B RID: 60507
		[DispId(-2147418089)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondragover([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC5C RID: 60508
		[DispId(-2147418088)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondragleave([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC5D RID: 60509
		[DispId(-2147418087)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondrop([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC5E RID: 60510
		[DispId(-2147418083)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforecut([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC5F RID: 60511
		[DispId(-2147418086)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncut([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC60 RID: 60512
		[DispId(-2147418082)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforecopy([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC61 RID: 60513
		[DispId(-2147418085)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncopy([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC62 RID: 60514
		[DispId(-2147418081)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforepaste([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC63 RID: 60515
		[DispId(-2147418084)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onpaste([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC64 RID: 60516
		[DispId(1023)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncontextmenu([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC65 RID: 60517
		[DispId(-2147418080)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowsdelete([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC66 RID: 60518
		[DispId(-2147418079)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowsinserted([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC67 RID: 60519
		[DispId(-2147418078)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void oncellchange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC68 RID: 60520
		[DispId(-609)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onreadystatechange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC69 RID: 60521
		[DispId(1030)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onlayoutcomplete([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC6A RID: 60522
		[DispId(1031)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onpage([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC6B RID: 60523
		[DispId(1042)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseenter([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC6C RID: 60524
		[DispId(1043)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseleave([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC6D RID: 60525
		[DispId(1044)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC6E RID: 60526
		[DispId(1045)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondeactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC6F RID: 60527
		[DispId(1034)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforedeactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC70 RID: 60528
		[DispId(1047)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforeactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC71 RID: 60529
		[DispId(1048)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocusin([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC72 RID: 60530
		[DispId(1049)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocusout([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC73 RID: 60531
		[DispId(1035)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmove([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC74 RID: 60532
		[DispId(1036)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncontrolselect([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC75 RID: 60533
		[DispId(1038)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onmovestart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC76 RID: 60534
		[DispId(1039)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmoveend([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC77 RID: 60535
		[DispId(1040)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onresizestart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC78 RID: 60536
		[DispId(1041)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onresizeend([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC79 RID: 60537
		[DispId(1033)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onmousewheel([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);
	}
}
