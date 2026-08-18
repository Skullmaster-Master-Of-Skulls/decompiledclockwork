using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000571 RID: 1393
	[TypeLibType(4112)]
	[Guid("3050F617-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(2)]
	[ComImport]
	public interface HTMLButtonElementEvents2
	{
		// Token: 0x06008A58 RID: 35416
		[DispId(-2147418102)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onhelp([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A59 RID: 35417
		[DispId(-600)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onclick([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A5A RID: 35418
		[DispId(-601)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondblclick([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A5B RID: 35419
		[DispId(-603)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onkeypress([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A5C RID: 35420
		[DispId(-602)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onkeydown([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A5D RID: 35421
		[DispId(-604)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onkeyup([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A5E RID: 35422
		[DispId(-2147418103)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseout([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A5F RID: 35423
		[DispId(-2147418104)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseover([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A60 RID: 35424
		[DispId(-606)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmousemove([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A61 RID: 35425
		[DispId(-605)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmousedown([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A62 RID: 35426
		[DispId(-607)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseup([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A63 RID: 35427
		[DispId(-2147418100)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onselectstart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A64 RID: 35428
		[DispId(-2147418095)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfilterchange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A65 RID: 35429
		[DispId(-2147418101)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondragstart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A66 RID: 35430
		[DispId(-2147418108)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforeupdate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A67 RID: 35431
		[DispId(-2147418107)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onafterupdate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A68 RID: 35432
		[DispId(-2147418099)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onerrorupdate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A69 RID: 35433
		[DispId(-2147418106)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onrowexit([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A6A RID: 35434
		[DispId(-2147418105)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowenter([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A6B RID: 35435
		[DispId(-2147418098)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondatasetchanged([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A6C RID: 35436
		[DispId(-2147418097)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondataavailable([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A6D RID: 35437
		[DispId(-2147418096)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondatasetcomplete([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A6E RID: 35438
		[DispId(-2147418094)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onlosecapture([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A6F RID: 35439
		[DispId(-2147418093)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onpropertychange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A70 RID: 35440
		[DispId(1014)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onscroll([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A71 RID: 35441
		[DispId(-2147418111)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocus([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A72 RID: 35442
		[DispId(-2147418112)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onblur([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A73 RID: 35443
		[DispId(1016)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onresize([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A74 RID: 35444
		[DispId(-2147418092)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondrag([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A75 RID: 35445
		[DispId(-2147418091)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondragend([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A76 RID: 35446
		[DispId(-2147418090)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondragenter([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A77 RID: 35447
		[DispId(-2147418089)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondragover([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A78 RID: 35448
		[DispId(-2147418088)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondragleave([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A79 RID: 35449
		[DispId(-2147418087)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondrop([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A7A RID: 35450
		[DispId(-2147418083)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforecut([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A7B RID: 35451
		[DispId(-2147418086)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncut([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A7C RID: 35452
		[DispId(-2147418082)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforecopy([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A7D RID: 35453
		[DispId(-2147418085)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncopy([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A7E RID: 35454
		[DispId(-2147418081)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforepaste([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A7F RID: 35455
		[DispId(-2147418084)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onpaste([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A80 RID: 35456
		[DispId(1023)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncontextmenu([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A81 RID: 35457
		[DispId(-2147418080)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowsdelete([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A82 RID: 35458
		[DispId(-2147418079)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowsinserted([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A83 RID: 35459
		[DispId(-2147418078)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void oncellchange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A84 RID: 35460
		[DispId(-609)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onreadystatechange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A85 RID: 35461
		[DispId(1030)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onlayoutcomplete([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A86 RID: 35462
		[DispId(1031)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onpage([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A87 RID: 35463
		[DispId(1042)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseenter([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A88 RID: 35464
		[DispId(1043)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseleave([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A89 RID: 35465
		[DispId(1044)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A8A RID: 35466
		[DispId(1045)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondeactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A8B RID: 35467
		[DispId(1034)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforedeactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A8C RID: 35468
		[DispId(1047)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforeactivate([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A8D RID: 35469
		[DispId(1048)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocusin([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A8E RID: 35470
		[DispId(1049)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocusout([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A8F RID: 35471
		[DispId(1035)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmove([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A90 RID: 35472
		[DispId(1036)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool oncontrolselect([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A91 RID: 35473
		[DispId(1038)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onmovestart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A92 RID: 35474
		[DispId(1039)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmoveend([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A93 RID: 35475
		[DispId(1040)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onresizestart([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A94 RID: 35476
		[DispId(1041)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onresizeend([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06008A95 RID: 35477
		[DispId(1033)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onmousewheel([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);
	}
}
