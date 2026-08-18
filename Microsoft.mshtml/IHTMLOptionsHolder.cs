using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BF4 RID: 3060
	[Guid("3050F378-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLOptionsHolder
	{
		// Token: 0x170073EF RID: 29679
		// (get) Token: 0x06015B01 RID: 88833
		[DispId(1503)]
		IHTMLDocument2 document { [DispId(1503)] [TypeLibFunc(64)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x170073F0 RID: 29680
		// (get) Token: 0x06015B02 RID: 88834
		[DispId(1504)]
		IHTMLFontNamesCollection fonts { [TypeLibFunc(64)] [DispId(1504)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x170073F1 RID: 29681
		// (get) Token: 0x06015B04 RID: 88836
		// (set) Token: 0x06015B03 RID: 88835
		[DispId(1505)]
		object execArg { [DispId(1505)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(1505)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x170073F2 RID: 29682
		// (get) Token: 0x06015B06 RID: 88838
		// (set) Token: 0x06015B05 RID: 88837
		[DispId(1506)]
		int errorLine { [DispId(1506)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1506)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x170073F3 RID: 29683
		// (get) Token: 0x06015B08 RID: 88840
		// (set) Token: 0x06015B07 RID: 88839
		[DispId(1507)]
		int errorCharacter { [DispId(1507)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1507)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x170073F4 RID: 29684
		// (get) Token: 0x06015B0A RID: 88842
		// (set) Token: 0x06015B09 RID: 88841
		[DispId(1508)]
		int errorCode { [DispId(1508)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1508)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x170073F5 RID: 29685
		// (get) Token: 0x06015B0C RID: 88844
		// (set) Token: 0x06015B0B RID: 88843
		[DispId(1509)]
		string errorMessage { [DispId(1509)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1509)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x170073F6 RID: 29686
		// (get) Token: 0x06015B0E RID: 88846
		// (set) Token: 0x06015B0D RID: 88845
		[DispId(1510)]
		bool errorDebug { [DispId(1510)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1510)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x170073F7 RID: 29687
		// (get) Token: 0x06015B0F RID: 88847
		[DispId(1511)]
		IHTMLWindow2 unsecuredWindowOfDocument { [DispId(1511)] [TypeLibFunc(64)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x170073F8 RID: 29688
		// (get) Token: 0x06015B11 RID: 88849
		// (set) Token: 0x06015B10 RID: 88848
		[DispId(1512)]
		string findText { [DispId(1512)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1512)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x170073F9 RID: 29689
		// (get) Token: 0x06015B13 RID: 88851
		// (set) Token: 0x06015B12 RID: 88850
		[DispId(1513)]
		bool anythingAfterFrameset { [DispId(1513)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1513)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x06015B14 RID: 88852
		[DispId(1514)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLFontSizesCollection sizes([MarshalAs(UnmanagedType.BStr)] [In] string fontName);

		// Token: 0x06015B15 RID: 88853
		[DispId(1515)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string openfiledlg([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object initFile, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object initDir, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object filter, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object title);

		// Token: 0x06015B16 RID: 88854
		[DispId(1516)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string savefiledlg([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object initFile, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object initDir, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object filter, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object title);

		// Token: 0x06015B17 RID: 88855
		[DispId(1517)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		int choosecolordlg([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object initColor);

		// Token: 0x06015B18 RID: 88856
		[DispId(1518)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void showSecurityInfo();

		// Token: 0x06015B19 RID: 88857
		[DispId(1519)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool isApartmentModel([MarshalAs(UnmanagedType.Interface)] [In] IHTMLObjectElement @object);

		// Token: 0x06015B1A RID: 88858
		[DispId(1520)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		int getCharset([MarshalAs(UnmanagedType.BStr)] [In] string fontName);

		// Token: 0x170073FA RID: 29690
		// (get) Token: 0x06015B1B RID: 88859
		[DispId(1521)]
		string secureConnectionInfo { [DispId(1521)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }
	}
}
