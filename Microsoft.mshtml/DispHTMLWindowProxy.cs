using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007DC RID: 2012
	[Guid("3050F55E-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(2)]
	[DefaultMember("item")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLWindowProxy
	{
		// Token: 0x0600D8F2 RID: 55538
		[DispId(0)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object item([MarshalAs(UnmanagedType.Struct)] [In] ref object pvarIndex);

		// Token: 0x1700483B RID: 18491
		// (get) Token: 0x0600D8F3 RID: 55539
		[DispId(1001)]
		int length { [DispId(1001)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700483C RID: 18492
		// (get) Token: 0x0600D8F4 RID: 55540
		[DispId(1100)]
		FramesCollection frames { [DispId(1100)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700483D RID: 18493
		// (get) Token: 0x0600D8F6 RID: 55542
		// (set) Token: 0x0600D8F5 RID: 55541
		[DispId(1101)]
		string defaultStatus { [DispId(1101)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1101)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x1700483E RID: 18494
		// (get) Token: 0x0600D8F8 RID: 55544
		// (set) Token: 0x0600D8F7 RID: 55543
		[DispId(1102)]
		string status { [DispId(1102)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1102)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x0600D8F9 RID: 55545
		[DispId(1104)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void clearTimeout([In] int timerID);

		// Token: 0x0600D8FA RID: 55546
		[DispId(1105)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void alert([MarshalAs(UnmanagedType.BStr)] [In] string message = "");

		// Token: 0x0600D8FB RID: 55547
		[DispId(1110)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool confirm([MarshalAs(UnmanagedType.BStr)] [In] string message = "");

		// Token: 0x0600D8FC RID: 55548
		[DispId(1111)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object prompt([MarshalAs(UnmanagedType.BStr)] [In] string message = "", [MarshalAs(UnmanagedType.BStr)] [In] string defstr = "undefined");

		// Token: 0x1700483F RID: 18495
		// (get) Token: 0x0600D8FD RID: 55549
		[DispId(1125)]
		HTMLImageElementFactory Image { [DispId(1125)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004840 RID: 18496
		// (get) Token: 0x0600D8FE RID: 55550
		[DispId(14)]
		HTMLLocation location { [DispId(14)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004841 RID: 18497
		// (get) Token: 0x0600D8FF RID: 55551
		[DispId(2)]
		HTMLHistory history { [DispId(2)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600D900 RID: 55552
		[DispId(3)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void close();

		// Token: 0x17004842 RID: 18498
		// (get) Token: 0x0600D902 RID: 55554
		// (set) Token: 0x0600D901 RID: 55553
		[DispId(4)]
		object opener { [DispId(4)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(4)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17004843 RID: 18499
		// (get) Token: 0x0600D903 RID: 55555
		[DispId(5)]
		HTMLNavigator navigator { [DispId(5)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004844 RID: 18500
		// (get) Token: 0x0600D905 RID: 55557
		// (set) Token: 0x0600D904 RID: 55556
		[DispId(11)]
		string name { [DispId(11)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(11)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17004845 RID: 18501
		// (get) Token: 0x0600D906 RID: 55558
		[DispId(12)]
		IHTMLWindow2 parent { [DispId(12)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600D907 RID: 55559
		[DispId(13)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLWindow2 open([MarshalAs(UnmanagedType.BStr)] [In] string url = "", [MarshalAs(UnmanagedType.BStr)] [In] string name = "", [MarshalAs(UnmanagedType.BStr)] [In] string features = "", [In] bool replace = false);

		// Token: 0x17004846 RID: 18502
		// (get) Token: 0x0600D908 RID: 55560
		[DispId(20)]
		IHTMLWindow2 self { [DispId(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004847 RID: 18503
		// (get) Token: 0x0600D909 RID: 55561
		[DispId(21)]
		IHTMLWindow2 top { [DispId(21)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004848 RID: 18504
		// (get) Token: 0x0600D90A RID: 55562
		[DispId(22)]
		IHTMLWindow2 window { [DispId(22)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600D90B RID: 55563
		[DispId(25)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void navigate([MarshalAs(UnmanagedType.BStr)] [In] string url);

		// Token: 0x17004849 RID: 18505
		// (get) Token: 0x0600D90D RID: 55565
		// (set) Token: 0x0600D90C RID: 55564
		[DispId(-2147412098)]
		object onfocus { [DispId(-2147412098)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412098)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x1700484A RID: 18506
		// (get) Token: 0x0600D90F RID: 55567
		// (set) Token: 0x0600D90E RID: 55566
		[DispId(-2147412097)]
		object onblur { [DispId(-2147412097)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412097)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x1700484B RID: 18507
		// (get) Token: 0x0600D911 RID: 55569
		// (set) Token: 0x0600D910 RID: 55568
		[DispId(-2147412080)]
		object onload { [TypeLibFunc(20)] [DispId(-2147412080)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412080)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x1700484C RID: 18508
		// (get) Token: 0x0600D913 RID: 55571
		// (set) Token: 0x0600D912 RID: 55570
		[DispId(-2147412073)]
		object onbeforeunload { [TypeLibFunc(20)] [DispId(-2147412073)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412073)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x1700484D RID: 18509
		// (get) Token: 0x0600D915 RID: 55573
		// (set) Token: 0x0600D914 RID: 55572
		[DispId(-2147412079)]
		object onunload { [DispId(-2147412079)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412079)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x1700484E RID: 18510
		// (get) Token: 0x0600D917 RID: 55575
		// (set) Token: 0x0600D916 RID: 55574
		[DispId(-2147412099)]
		object onhelp { [DispId(-2147412099)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412099)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x1700484F RID: 18511
		// (get) Token: 0x0600D919 RID: 55577
		// (set) Token: 0x0600D918 RID: 55576
		[DispId(-2147412083)]
		object onerror { [DispId(-2147412083)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412083)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17004850 RID: 18512
		// (get) Token: 0x0600D91B RID: 55579
		// (set) Token: 0x0600D91A RID: 55578
		[DispId(-2147412076)]
		object onresize { [TypeLibFunc(20)] [DispId(-2147412076)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412076)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17004851 RID: 18513
		// (get) Token: 0x0600D91D RID: 55581
		// (set) Token: 0x0600D91C RID: 55580
		[DispId(-2147412081)]
		object onscroll { [TypeLibFunc(20)] [DispId(-2147412081)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412081)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17004852 RID: 18514
		// (get) Token: 0x0600D91E RID: 55582
		[DispId(1151)]
		IHTMLDocument2 document { [DispId(1151)] [TypeLibFunc(2)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004853 RID: 18515
		// (get) Token: 0x0600D91F RID: 55583
		[DispId(1152)]
		IHTMLEventObj @event { [DispId(1152)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004854 RID: 18516
		// (get) Token: 0x0600D920 RID: 55584
		[DispId(1153)]
		object _newEnum { [TypeLibFunc(65)] [DispId(1153)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IUnknown)] get; }

		// Token: 0x0600D921 RID: 55585
		[DispId(1154)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object showModalDialog([MarshalAs(UnmanagedType.BStr)] [In] string dialog, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object varArgIn, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object varOptions);

		// Token: 0x0600D922 RID: 55586
		[DispId(1155)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void showHelp([MarshalAs(UnmanagedType.BStr)] [In] string helpURL, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object helpArg, [MarshalAs(UnmanagedType.BStr)] [In] string features = "");

		// Token: 0x17004855 RID: 18517
		// (get) Token: 0x0600D923 RID: 55587
		[DispId(1156)]
		IHTMLScreen screen { [DispId(1156)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004856 RID: 18518
		// (get) Token: 0x0600D924 RID: 55588
		[DispId(1157)]
		HTMLOptionElementFactory Option { [DispId(1157)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600D925 RID: 55589
		[DispId(1158)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void focus();

		// Token: 0x17004857 RID: 18519
		// (get) Token: 0x0600D926 RID: 55590
		[DispId(23)]
		bool closed { [DispId(23)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600D927 RID: 55591
		[DispId(1159)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void blur();

		// Token: 0x0600D928 RID: 55592
		[DispId(1160)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void scroll([In] int x, [In] int y);

		// Token: 0x17004858 RID: 18520
		// (get) Token: 0x0600D929 RID: 55593
		[DispId(1161)]
		HTMLNavigator clientInformation { [DispId(1161)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600D92A RID: 55594
		[DispId(1163)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void clearInterval([In] int timerID);

		// Token: 0x17004859 RID: 18521
		// (get) Token: 0x0600D92C RID: 55596
		// (set) Token: 0x0600D92B RID: 55595
		[DispId(1164)]
		object offscreenBuffering { [DispId(1164)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(1164)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x0600D92D RID: 55597
		[DispId(1165)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object execScript([MarshalAs(UnmanagedType.BStr)] [In] string code, [MarshalAs(UnmanagedType.BStr)] [In] string language = "JScript");

		// Token: 0x0600D92E RID: 55598
		[DispId(1166)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string toString();

		// Token: 0x0600D92F RID: 55599
		[DispId(1167)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void scrollBy([In] int x, [In] int y);

		// Token: 0x0600D930 RID: 55600
		[DispId(1168)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void scrollTo([In] int x, [In] int y);

		// Token: 0x0600D931 RID: 55601
		[DispId(6)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void moveTo([In] int x, [In] int y);

		// Token: 0x0600D932 RID: 55602
		[DispId(7)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void moveBy([In] int x, [In] int y);

		// Token: 0x0600D933 RID: 55603
		[DispId(9)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void resizeTo([In] int x, [In] int y);

		// Token: 0x0600D934 RID: 55604
		[DispId(8)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void resizeBy([In] int x, [In] int y);

		// Token: 0x1700485A RID: 18522
		// (get) Token: 0x0600D935 RID: 55605
		[DispId(1169)]
		object external { [DispId(1169)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x1700485B RID: 18523
		// (get) Token: 0x0600D936 RID: 55606
		[DispId(1170)]
		int screenLeft { [DispId(1170)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700485C RID: 18524
		// (get) Token: 0x0600D937 RID: 55607
		[DispId(1171)]
		int screenTop { [DispId(1171)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600D938 RID: 55608
		[DispId(-2147417605)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool attachEvent([MarshalAs(UnmanagedType.BStr)] [In] string @event, [MarshalAs(UnmanagedType.IDispatch)] [In] object pdisp);

		// Token: 0x0600D939 RID: 55609
		[DispId(-2147417604)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void detachEvent([MarshalAs(UnmanagedType.BStr)] [In] string @event, [MarshalAs(UnmanagedType.IDispatch)] [In] object pdisp);

		// Token: 0x0600D93A RID: 55610
		[DispId(1103)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int setTimeout([MarshalAs(UnmanagedType.Struct)] [In] ref object expression, [In] int msec, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object language);

		// Token: 0x0600D93B RID: 55611
		[DispId(1162)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int setInterval([MarshalAs(UnmanagedType.Struct)] [In] ref object expression, [In] int msec, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object language);

		// Token: 0x0600D93C RID: 55612
		[DispId(1174)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void print();

		// Token: 0x1700485D RID: 18525
		// (get) Token: 0x0600D93E RID: 55614
		// (set) Token: 0x0600D93D RID: 55613
		[DispId(-2147412046)]
		object onbeforeprint { [TypeLibFunc(20)] [DispId(-2147412046)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412046)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x1700485E RID: 18526
		// (get) Token: 0x0600D940 RID: 55616
		// (set) Token: 0x0600D93F RID: 55615
		[DispId(-2147412045)]
		object onafterprint { [DispId(-2147412045)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412045)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x1700485F RID: 18527
		// (get) Token: 0x0600D941 RID: 55617
		[DispId(1175)]
		IHTMLDataTransfer clipboardData { [DispId(1175)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600D942 RID: 55618
		[DispId(1176)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLWindow2 showModelessDialog([MarshalAs(UnmanagedType.BStr)] [In] string url = "", [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object varArgIn, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object options);

		// Token: 0x0600D943 RID: 55619
		[DispId(1180)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object createPopup([MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object varArgIn);

		// Token: 0x17004860 RID: 18528
		// (get) Token: 0x0600D944 RID: 55620
		[DispId(1181)]
		IHTMLFrameBase frameElement { [DispId(1181)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
