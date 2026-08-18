using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007B6 RID: 1974
	[DefaultMember("item")]
	[Guid("332C4427-26CB-11D0-B483-00C04FD90119")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLWindow2 : IHTMLFramesCollection2
	{
		// Token: 0x0600D6CF RID: 54991
		[DispId(0)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object item([MarshalAs(UnmanagedType.Struct)] [In] ref object pvarIndex);

		// Token: 0x17004769 RID: 18281
		// (get) Token: 0x0600D6D0 RID: 54992
		[DispId(1001)]
		int length { [DispId(1001)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700476A RID: 18282
		// (get) Token: 0x0600D6D1 RID: 54993
		[DispId(1100)]
		FramesCollection frames { [DispId(1100)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700476B RID: 18283
		// (get) Token: 0x0600D6D3 RID: 54995
		// (set) Token: 0x0600D6D2 RID: 54994
		[DispId(1101)]
		string defaultStatus { [DispId(1101)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1101)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x1700476C RID: 18284
		// (get) Token: 0x0600D6D5 RID: 54997
		// (set) Token: 0x0600D6D4 RID: 54996
		[DispId(1102)]
		string status { [DispId(1102)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1102)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x0600D6D6 RID: 54998
		[DispId(1172)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		int setTimeout([MarshalAs(UnmanagedType.BStr)] [In] string expression, [In] int msec, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object language);

		// Token: 0x0600D6D7 RID: 54999
		[DispId(1104)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void clearTimeout([In] int timerID);

		// Token: 0x0600D6D8 RID: 55000
		[DispId(1105)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void alert([MarshalAs(UnmanagedType.BStr)] [In] string message = "");

		// Token: 0x0600D6D9 RID: 55001
		[DispId(1110)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool confirm([MarshalAs(UnmanagedType.BStr)] [In] string message = "");

		// Token: 0x0600D6DA RID: 55002
		[DispId(1111)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object prompt([MarshalAs(UnmanagedType.BStr)] [In] string message = "", [MarshalAs(UnmanagedType.BStr)] [In] string defstr = "undefined");

		// Token: 0x1700476D RID: 18285
		// (get) Token: 0x0600D6DB RID: 55003
		[DispId(1125)]
		HTMLImageElementFactory Image { [DispId(1125)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700476E RID: 18286
		// (get) Token: 0x0600D6DC RID: 55004
		[DispId(14)]
		HTMLLocation location { [DispId(14)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700476F RID: 18287
		// (get) Token: 0x0600D6DD RID: 55005
		[DispId(2)]
		HTMLHistory history { [DispId(2)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600D6DE RID: 55006
		[DispId(3)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void close();

		// Token: 0x17004770 RID: 18288
		// (get) Token: 0x0600D6E0 RID: 55008
		// (set) Token: 0x0600D6DF RID: 55007
		[DispId(4)]
		object opener { [DispId(4)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(4)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x17004771 RID: 18289
		// (get) Token: 0x0600D6E1 RID: 55009
		[DispId(5)]
		HTMLNavigator navigator { [DispId(5)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004772 RID: 18290
		// (get) Token: 0x0600D6E3 RID: 55011
		// (set) Token: 0x0600D6E2 RID: 55010
		[DispId(11)]
		string name { [DispId(11)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(11)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x17004773 RID: 18291
		// (get) Token: 0x0600D6E4 RID: 55012
		[DispId(12)]
		IHTMLWindow2 parent { [DispId(12)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600D6E5 RID: 55013
		[DispId(13)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLWindow2 open([MarshalAs(UnmanagedType.BStr)] [In] string url = "", [MarshalAs(UnmanagedType.BStr)] [In] string name = "", [MarshalAs(UnmanagedType.BStr)] [In] string features = "", [In] bool replace = false);

		// Token: 0x17004774 RID: 18292
		// (get) Token: 0x0600D6E6 RID: 55014
		[DispId(20)]
		IHTMLWindow2 self { [DispId(20)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004775 RID: 18293
		// (get) Token: 0x0600D6E7 RID: 55015
		[DispId(21)]
		IHTMLWindow2 top { [DispId(21)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004776 RID: 18294
		// (get) Token: 0x0600D6E8 RID: 55016
		[DispId(22)]
		IHTMLWindow2 window { [DispId(22)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600D6E9 RID: 55017
		[DispId(25)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void navigate([MarshalAs(UnmanagedType.BStr)] [In] string url);

		// Token: 0x17004777 RID: 18295
		// (get) Token: 0x0600D6EB RID: 55019
		// (set) Token: 0x0600D6EA RID: 55018
		[DispId(-2147412098)]
		object onfocus { [TypeLibFunc(20)] [DispId(-2147412098)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412098)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x17004778 RID: 18296
		// (get) Token: 0x0600D6ED RID: 55021
		// (set) Token: 0x0600D6EC RID: 55020
		[DispId(-2147412097)]
		object onblur { [DispId(-2147412097)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412097)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x17004779 RID: 18297
		// (get) Token: 0x0600D6EF RID: 55023
		// (set) Token: 0x0600D6EE RID: 55022
		[DispId(-2147412080)]
		object onload { [DispId(-2147412080)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412080)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x1700477A RID: 18298
		// (get) Token: 0x0600D6F1 RID: 55025
		// (set) Token: 0x0600D6F0 RID: 55024
		[DispId(-2147412073)]
		object onbeforeunload { [DispId(-2147412073)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412073)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x1700477B RID: 18299
		// (get) Token: 0x0600D6F3 RID: 55027
		// (set) Token: 0x0600D6F2 RID: 55026
		[DispId(-2147412079)]
		object onunload { [TypeLibFunc(20)] [DispId(-2147412079)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412079)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x1700477C RID: 18300
		// (get) Token: 0x0600D6F5 RID: 55029
		// (set) Token: 0x0600D6F4 RID: 55028
		[DispId(-2147412099)]
		object onhelp { [TypeLibFunc(20)] [DispId(-2147412099)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412099)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x1700477D RID: 18301
		// (get) Token: 0x0600D6F7 RID: 55031
		// (set) Token: 0x0600D6F6 RID: 55030
		[DispId(-2147412083)]
		object onerror { [DispId(-2147412083)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412083)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x1700477E RID: 18302
		// (get) Token: 0x0600D6F9 RID: 55033
		// (set) Token: 0x0600D6F8 RID: 55032
		[DispId(-2147412076)]
		object onresize { [DispId(-2147412076)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412076)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x1700477F RID: 18303
		// (get) Token: 0x0600D6FB RID: 55035
		// (set) Token: 0x0600D6FA RID: 55034
		[DispId(-2147412081)]
		object onscroll { [DispId(-2147412081)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412081)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x17004780 RID: 18304
		// (get) Token: 0x0600D6FC RID: 55036
		[DispId(1151)]
		IHTMLDocument2 document { [TypeLibFunc(2)] [DispId(1151)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004781 RID: 18305
		// (get) Token: 0x0600D6FD RID: 55037
		[DispId(1152)]
		IHTMLEventObj @event { [DispId(1152)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004782 RID: 18306
		// (get) Token: 0x0600D6FE RID: 55038
		[DispId(1153)]
		object _newEnum { [TypeLibFunc(65)] [DispId(1153)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IUnknown)] get; }

		// Token: 0x0600D6FF RID: 55039
		[DispId(1154)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object showModalDialog([MarshalAs(UnmanagedType.BStr)] [In] string dialog, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object varArgIn, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object varOptions);

		// Token: 0x0600D700 RID: 55040
		[DispId(1155)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void showHelp([MarshalAs(UnmanagedType.BStr)] [In] string helpURL, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object helpArg, [MarshalAs(UnmanagedType.BStr)] [In] string features = "");

		// Token: 0x17004783 RID: 18307
		// (get) Token: 0x0600D701 RID: 55041
		[DispId(1156)]
		IHTMLScreen screen { [DispId(1156)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17004784 RID: 18308
		// (get) Token: 0x0600D702 RID: 55042
		[DispId(1157)]
		HTMLOptionElementFactory Option { [DispId(1157)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600D703 RID: 55043
		[DispId(1158)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void focus();

		// Token: 0x17004785 RID: 18309
		// (get) Token: 0x0600D704 RID: 55044
		[DispId(23)]
		bool closed { [DispId(23)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600D705 RID: 55045
		[DispId(1159)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void blur();

		// Token: 0x0600D706 RID: 55046
		[DispId(1160)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void scroll([In] int x, [In] int y);

		// Token: 0x17004786 RID: 18310
		// (get) Token: 0x0600D707 RID: 55047
		[DispId(1161)]
		HTMLNavigator clientInformation { [DispId(1161)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600D708 RID: 55048
		[DispId(1173)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		int setInterval([MarshalAs(UnmanagedType.BStr)] [In] string expression, [In] int msec, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object language);

		// Token: 0x0600D709 RID: 55049
		[DispId(1163)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void clearInterval([In] int timerID);

		// Token: 0x17004787 RID: 18311
		// (get) Token: 0x0600D70B RID: 55051
		// (set) Token: 0x0600D70A RID: 55050
		[DispId(1164)]
		object offscreenBuffering { [DispId(1164)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(1164)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x0600D70C RID: 55052
		[DispId(1165)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object execScript([MarshalAs(UnmanagedType.BStr)] [In] string code, [MarshalAs(UnmanagedType.BStr)] [In] string language = "JScript");

		// Token: 0x0600D70D RID: 55053
		[DispId(1166)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string toString();

		// Token: 0x0600D70E RID: 55054
		[DispId(1167)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void scrollBy([In] int x, [In] int y);

		// Token: 0x0600D70F RID: 55055
		[DispId(1168)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void scrollTo([In] int x, [In] int y);

		// Token: 0x0600D710 RID: 55056
		[DispId(6)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void moveTo([In] int x, [In] int y);

		// Token: 0x0600D711 RID: 55057
		[DispId(7)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void moveBy([In] int x, [In] int y);

		// Token: 0x0600D712 RID: 55058
		[DispId(9)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void resizeTo([In] int x, [In] int y);

		// Token: 0x0600D713 RID: 55059
		[DispId(8)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void resizeBy([In] int x, [In] int y);

		// Token: 0x17004788 RID: 18312
		// (get) Token: 0x0600D714 RID: 55060
		[DispId(1169)]
		object external { [DispId(1169)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }
	}
}
